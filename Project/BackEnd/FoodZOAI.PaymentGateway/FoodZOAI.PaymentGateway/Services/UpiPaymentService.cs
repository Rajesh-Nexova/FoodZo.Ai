using FoodZOAI.PaymentGateway.Configuration;
using FoodZOAI.PaymentGateway.CustomException;
using FoodZOAI.PaymentGateway.Models;
using FoodZOAI.PaymentGateway.Security.Services;
using System.Text.Json;

namespace FoodZOAI.PaymentGateway.Services
{
    public class UpiPaymentService : BasePaymentGatewayService
    {
        public UpiPaymentService(
            HttpClient httpClient,
            ILogger<UpiPaymentService> logger,
            PaymentGatewayOptions options,
            ISecurityService securityService)
            : base(httpClient, logger, options, securityService)
        {
        }

        protected override void AddAuthenticationHeaders(HttpRequestMessage request)
        {
            request.Headers.Add("x-api-key", _options.ApiKey);
            request.Headers.Add("x-timestamp", DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString());
            var payload = request.Content?.ReadAsStringAsync().Result ?? "";
            request.Headers.Add("x-signature", _securityService.ComputeHmacSha256(payload, _options.SecretKey));
        }

        public override async Task<PaymentResponse> InitiatePaymentAsync(PaymentRequest request)
        {
            if (request.PaymentMethod != PaymentMethod.Upi)
            {
                throw new PaymentGatewayException("Invalid payment method", "This service only supports UPI payments");
            }

            _logger.LogInformation("Initiating UPI payment for order {OrderId}", request.OrderId);

            var payload = new
            {
                merchantId = _options.MerchantId,
                merchantTransactionId = request.OrderId,
                amount = (long)(request.Amount * 100), // Convert to smallest currency unit
                merchantCallbackUrl = request.WebhookUrl,
                vpa = (request.MetaData.ContainsKey("vpa") ? request.MetaData["vpa"] : null),
                customerName = request.CustomerName,
                customerMobile = request.CustomerPhone,
                customerEmail = request.CustomerEmail
            };

            var response = await SendRequestAsync<dynamic>(HttpMethod.Post, "/api/v1/upi/qr/create", payload);

            var responseObj = new PaymentResponse
            {
                PaymentId = response.transactionId,
                OrderId = request.OrderId,
                Status = PaymentStatus.Created,
                Amount = request.Amount,
                Currency = request.Currency,
                RedirectUrl = response.data.qrCodeUrl,
                MetaData = new Dictionary<string, string>
                {
                    { "upiUrl", response.data.upiUrl },
                    { "qrCodeUrl", response.data.qrCodeUrl }
                }
            };

            _logger.LogInformation("UPI payment initiated. Order ID: {OrderId}, Transaction ID: {TransactionId}",
                request.OrderId, responseObj.PaymentId);

            return responseObj;
        }

        public override async Task<PaymentResponse> GetPaymentStatusAsync(string paymentId)
        {
            _logger.LogInformation("Getting UPI payment status for {PaymentId}", paymentId);

            var payload = new
            {
                merchantId = _options.MerchantId,
                transactionId = paymentId
            };

            var response = await SendRequestAsync<dynamic>(HttpMethod.Post, "/api/v1/transaction/status", payload);

            var responseObj = new PaymentResponse
            {
                PaymentId = response.transactionId,
                OrderId = response.merchantTransactionId,
                Status = MapStatus(response.status),
                Amount = (decimal)response.amount / 100,
                Currency = "INR",
                TransactionId = response.transactionId
            };

            _logger.LogInformation("UPI payment status retrieved. Transaction ID: {TransactionId}, Status: {Status}",
                responseObj.TransactionId, responseObj.Status);

            return responseObj;
        }

        // In UPI immediate capture usually happens, so this method is mainly for consistency
        public override Task<PaymentResponse> CapturePaymentAsync(string paymentId, decimal amount)
        {
            _logger.LogInformation("UPI payments are auto-captured, returning status for {PaymentId}", paymentId);
            return GetPaymentStatusAsync(paymentId);
        }

        public override async Task<PaymentResponse> RefundPaymentAsync(string paymentId, decimal amount, string reason = null)
        {
            _logger.LogInformation("Refunding UPI payment {PaymentId}, amount {Amount}", paymentId, amount);

            var payload = new
            {
                merchantId = _options.MerchantId,
                transactionId = paymentId,
                refundAmount = (long)(amount * 100),
                refundReason = reason ?? "Customer requested refund"
            };

            var response = await SendRequestAsync<dynamic>(HttpMethod.Post, "/api/v1/transaction/refund", payload);

            var responseObj = new PaymentResponse
            {
                PaymentId = paymentId,
                Status = PaymentStatus.Refunded,
                Amount = amount,
                Currency = "INR",
                TransactionId = response.refundId
            };

            //_logger.LogInformation("UPI payment refunded. Transaction ID: {TransactionId}, Refund ID: {RefundId}",
            //    paymentId, response.refundId);

            return responseObj;
        }

        public override Task<bool> ValidateWebhookSignatureAsync(string payload, string signature)
        {
            _logger.LogInformation("Validating UPI webhook signature");

            try
            {
                var expectedSignature = _securityService.ComputeHmacSha256(payload, _options.WebhookSecret);
                return Task.FromResult(signature == expectedSignature);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating UPI webhook signature");
                return Task.FromResult(false);
            }
        }

        public override WebhookEvent ParseWebhookEvent(string payload)
        {
            try
            {
                var jsonData = JsonSerializer.Deserialize<JsonElement>(payload);
                var eventType = jsonData.GetProperty("event").GetString();
                var transactionId = jsonData.GetProperty("transactionId").GetString();
                var merchantTransactionId = jsonData.GetProperty("merchantTransactionId").GetString();
                var status = jsonData.GetProperty("status").GetString();
                var amount = (decimal)jsonData.GetProperty("amount").GetInt64() / 100;

                return new WebhookEvent
                {
                    EventType = eventType,
                    PaymentId = transactionId,
                    OrderId = merchantTransactionId,
                    Status = MapStatus(status),
                    Amount = amount,
                    Currency = "INR",
                    Timestamp = DateTime.UtcNow
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error parsing UPI webhook event");
                throw new PaymentGatewayException("Error parsing webhook", ex.Message);
            }
        }

        private PaymentStatus MapStatus(string providerStatus)
        {
            return providerStatus.ToLower() switch
            {
                "created" => PaymentStatus.Created,
                "authorized" => PaymentStatus.Authorized,
                "captured" => PaymentStatus.Captured,
                "refunded" => PaymentStatus.Refunded,
                "failed" => PaymentStatus.Failed,
                _ => PaymentStatus.Pending
            };
        }
    }
}
