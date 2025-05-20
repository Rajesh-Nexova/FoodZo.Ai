using FoodZOAI.PaymentGateway.Configuration;
using FoodZOAI.PaymentGateway.Models;
using FoodZOAI.PaymentGateway.Security.Services;
using System.Text.Json;
using System.Text;
using FoodZOAI.PaymentGateway.CustomException;

namespace FoodZOAI.PaymentGateway.Services
{
    public class RazorPayGatewayService : BasePaymentGatewayService
    {
        public RazorPayGatewayService(
            HttpClient httpClient,
            ILogger<RazorPayGatewayService> logger,
            PaymentGatewayOptions options,
            ISecurityService securityService)
            : base(httpClient, logger, options, securityService)
        {
        }

        protected override void AddAuthenticationHeaders(HttpRequestMessage request)
        {
            var authString = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_options.ApiKey}:{_options.SecretKey}"));
            request.Headers.Add("Authorization", $"Basic {authString}");
        }

        public override async Task<PaymentResponse> InitiatePaymentAsync(PaymentRequest request)
        {
            _logger.LogInformation("Initiating payment for order {OrderId}, amount {Amount}",
                request.OrderId, request.Amount);

            // Create request payload for the specific provider
            var payload = new
            {
                amount = (long)(request.Amount * 100), // Convert to smallest currency unit (paise)
                currency = request.Currency,
                receipt = request.OrderId,
                notes = request.MetaData,
                payment_capture = 0 // Manual capture
            };

            var response = await SendRequestAsync<dynamic>(HttpMethod.Post, "/v1/orders", payload);

            var responseObj = new PaymentResponse
            {
                PaymentId = response.id,
                OrderId = request.OrderId,
                Status = PaymentStatus.Created,
                Amount = request.Amount,
                Currency = request.Currency,
                RedirectUrl = CreateCheckoutUrl(response.id, request)
            };

            _logger.LogInformation("Payment initiated. Order ID: {OrderId}, Payment ID: {PaymentId}",
                request.OrderId, responseObj.PaymentId);

            return responseObj;
        }

        private string CreateCheckoutUrl(string orderId, PaymentRequest request)
        {
            // Create checkout URL based on payment method
            var baseUrl = _options.IsSandbox ? "https://checkout-sandbox.razorpay.com/v1/checkout.js" : "https://checkout.razorpay.com/v1/checkout.js";

            // In a real implementation, we'd return a URL that leads to a page that loads the checkout JS
            // and initializes it with these options
            return $"{baseUrl}?order_id={orderId}&key={_options.ApiKey}&name={Uri.EscapeDataString(request.CustomerName)}&description={Uri.EscapeDataString(request.Description)}&callback_url={Uri.EscapeDataString(request.RedirectUrl)}";
        }

        public override async Task<PaymentResponse> CapturePaymentAsync(string paymentId, decimal amount)
        {
            _logger.LogInformation("Capturing payment {PaymentId}, amount {Amount}", paymentId, amount);

            var payload = new
            {
                amount = (long)(amount * 100), // Convert to smallest currency unit
                currency = "INR"
            };

            var response = await SendRequestAsync<dynamic>(HttpMethod.Post, $"/v1/payments/{paymentId}/capture", payload);

            var responseObj = new PaymentResponse
            {
                PaymentId = response.id,
                OrderId = response.order_id,
                Status = MapStatus(response.status),
                Amount = (decimal)response.amount / 100,
                Currency = response.currency,
                TransactionId = response.id
            };

            _logger.LogInformation("Payment captured. Payment ID: {PaymentId}, Status: {Status}",
                responseObj.PaymentId, responseObj.Status);

            return responseObj;
        }

        public override async Task<PaymentResponse> RefundPaymentAsync(string paymentId, decimal amount, string reason = null)
        {
            _logger.LogInformation("Refunding payment {PaymentId}, amount {Amount}, reason: {Reason}",
                paymentId, amount, reason);

            var payload = new
            {
                amount = (long)(amount * 100), // Convert to smallest currency unit
                notes = new Dictionary<string, string>
                {
                    { "reason", reason ?? "Customer requested refund" }
                }
            };

            var response = await SendRequestAsync<dynamic>(HttpMethod.Post, $"/v1/payments/{paymentId}/refund", payload);

            var responseObj = new PaymentResponse
            {
                PaymentId = response.payment_id,
                Status = PaymentStatus.Refunded,
                Amount = (decimal)response.amount / 100,
                Currency = response.currency,
                TransactionId = response.id
            };

            //need to Implement Seri Log
            //_logger.LogInformation("Payment refunded. Payment ID: {PaymentId}, Refund ID: {RefundId}",
            //    responseObj.PaymentId, response.id);

            return responseObj;
        }

        public override async Task<PaymentResponse> GetPaymentStatusAsync(string paymentId)
        {
            _logger.LogInformation("Getting payment status for {PaymentId}", paymentId);

            var response = await SendRequestAsync<dynamic>(HttpMethod.Get, $"/v1/payments/{paymentId}");

            var responseObj = new PaymentResponse
            {
                PaymentId = response.id,
                OrderId = response.order_id,
                Status = MapStatus(response.status),
                Amount = (decimal)response.amount / 100,
                Currency = response.currency,
                TransactionId = response.id
            };

            _logger.LogInformation("Payment status retrieved. Payment ID: {PaymentId}, Status: {Status}",
                responseObj.PaymentId, responseObj.Status);

            return responseObj;
        }

        public override Task<bool> ValidateWebhookSignatureAsync(string payload, string signature)
        {
            _logger.LogInformation("Validating webhook signature");

            try
            {
                var expectedSignature = _securityService.ComputeHmacSha256(payload, _options.WebhookSecret);
                return Task.FromResult(signature == expectedSignature);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating webhook signature");
                return Task.FromResult(false);
            }
        }

        public override WebhookEvent ParseWebhookEvent(string payload)
        {
            try
            {
                var jsonData = JsonSerializer.Deserialize<JsonElement>(payload);
                var eventType = jsonData.GetProperty("event").GetString();
                var paymentId = jsonData.GetProperty("payload").GetProperty("payment").GetProperty("entity").GetProperty("id").GetString();
                var status = jsonData.GetProperty("payload").GetProperty("payment").GetProperty("entity").GetProperty("status").GetString();
                var amount = jsonData.GetProperty("payload").GetProperty("payment").GetProperty("entity").GetProperty("amount").GetDecimal() / 100;
                var currency = jsonData.GetProperty("payload").GetProperty("payment").GetProperty("entity").GetProperty("currency").GetString();

                return new WebhookEvent
                {
                    EventType = eventType,
                    PaymentId = paymentId,
                    Status = MapStatus(status),
                    Amount = amount,
                    Currency = currency,
                    Timestamp = DateTime.UtcNow
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error parsing webhook event");
                throw new PaymentGatewayException("Error parsing webhook", ex.Message);
            }
        }

        private PaymentStatus MapStatus(string providerStatus)
        {
            return providerStatus.ToUpper() switch
            {
                "SUCCESS" => PaymentStatus.Captured,
                "PENDING" => PaymentStatus.Pending,
                "FAILED" => PaymentStatus.Failed,
                "REFUNDED" => PaymentStatus.Refunded,
                _ => PaymentStatus.Pending
            };
        }
    }
}
