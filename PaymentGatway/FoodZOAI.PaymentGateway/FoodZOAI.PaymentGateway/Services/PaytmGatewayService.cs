using FoodZOAI.PaymentGateway.Configuration;
using FoodZOAI.PaymentGateway.CustomException;
using FoodZOAI.PaymentGateway.Models;
using FoodZOAI.PaymentGateway.Security.Services;
using System.Text.Json;

namespace FoodZOAI.PaymentGateway.Services
{
    public class PaytmGatewayService : BasePaymentGatewayService
    {
        public PaytmGatewayService(
            HttpClient httpClient,
            ILogger<PaytmGatewayService> logger,
            PaymentGatewayOptions options,
            ISecurityService securityService)
            : base(httpClient, logger, options, securityService)
        {
        }

        protected override void AddAuthenticationHeaders(HttpRequestMessage request)
        {
            // Paytm uses a different authentication method
            request.Headers.Add("mid", _options.MerchantId);
            request.Headers.Add("checksumhash", _securityService.ComputeHmacSha256(
                request.Content?.ReadAsStringAsync().Result ?? "", _options.SecretKey));
        }

        public override async Task<PaymentResponse> InitiatePaymentAsync(PaymentRequest request)
        {
            _logger.LogInformation("Initiating Paytm payment for order {OrderId}", request.OrderId);

            var orderId = $"ORDER_{DateTime.Now:yyyyMMddHHmmss}_{Guid.NewGuid().ToString().Substring(0, 5)}";

            var payload = new
            {
                MID = _options.MerchantId,
                OrderId = orderId,
                TxnAmount = new { Value = request.Amount.ToString("F2"), Currency = request.Currency },
                UserInfo = new { CustId = request.CustomerEmail },
                CallbackUrl = request.RedirectUrl
            };

            var response = await SendRequestAsync<dynamic>(HttpMethod.Post, "/order/initiate", payload);

            var responseObj = new PaymentResponse
            {
                PaymentId = response.txnToken,
                OrderId = orderId,
                Status = PaymentStatus.Created,
                Amount = request.Amount,
                Currency = request.Currency,
                RedirectUrl = CreateCheckoutUrl(response.txnToken, request, orderId)
            };

            //_logger.LogInformation("Paytm payment initiated. Order ID: {OrderId}, TxnToken: {TxnToken}",
            //    orderId, response.txnToken);

            return responseObj;
        }

        private string CreateCheckoutUrl(string txnToken, PaymentRequest request, string orderId)
        {
            // This would typically return a URL to a page that integrates with Paytm JS checkout
            var baseUrl = _options.IsSandbox
                ? "https://securegw-stage.paytm.in/theia/api/v1/showPaymentPage"
                : "https://securegw.paytm.in/theia/api/v1/showPaymentPage";

            return $"{baseUrl}?mid={_options.MerchantId}&orderId={orderId}&txnToken={txnToken}";
        }

        public override async Task<PaymentResponse> CapturePaymentAsync(string paymentId, decimal amount)
        {
            // For Paytm, assuming paymentId is the transaction token
            _logger.LogInformation("Capturing Paytm payment {PaymentId}", paymentId);

            var payload = new
            {
                mid = _options.MerchantId,
                txnToken = paymentId,
                amount = amount.ToString("F2")
            };

            var response = await SendRequestAsync<dynamic>(HttpMethod.Post, "/transaction/capture", payload);

            var responseObj = new PaymentResponse
            {
                PaymentId = response.txnId,
                OrderId = response.orderId,
                Status = MapStatus(response.resultInfo.resultStatus),
                Amount = decimal.Parse(response.txnAmount),
                Currency = "INR",
                TransactionId = response.txnId
            };

            _logger.LogInformation("Paytm payment captured. TxnId: {TxnId}, Status: {Status}",
                responseObj.TransactionId, responseObj.Status);

            return responseObj;
        }

        public override async Task<PaymentResponse> RefundPaymentAsync(string paymentId, decimal amount, string reason = null)
        {
            _logger.LogInformation("Refunding Paytm payment {PaymentId}, amount {Amount}", paymentId, amount);

            var refundId = $"REFUND_{DateTime.Now:yyyyMMddHHmmss}_{Guid.NewGuid().ToString().Substring(0, 5)}";

            var payload = new
            {
                mid = _options.MerchantId,
                txnId = paymentId,
                refId = refundId,
                refundAmount = amount.ToString("F2"),
                comments = reason ?? "Customer requested refund"
            };

            var response = await SendRequestAsync<dynamic>(HttpMethod.Post, "/refund/apply", payload);

            var responseObj = new PaymentResponse
            {
                PaymentId = paymentId,
                Status = PaymentStatus.Refunded,
                Amount = amount,
                Currency = "INR",
                TransactionId = response.refundId
            };

            //_logger.LogInformation("Paytm payment refunded. TxnId: {TxnId}, RefundId: {RefundId}",
            //    paymentId, response.refundId);

            return responseObj;
        }

        public override async Task<PaymentResponse> GetPaymentStatusAsync(string paymentId)
        {
            _logger.LogInformation("Getting Paytm payment status for {PaymentId}", paymentId);

            var payload = new
            {
                mid = _options.MerchantId,
                orderId = paymentId
            };

            var response = await SendRequestAsync<dynamic>(HttpMethod.Post, "/order/status", payload);

            var responseObj = new PaymentResponse
            {
                PaymentId = response.txnId,
                OrderId = response.orderId,
                Status = MapStatus(response.resultInfo.resultStatus),
                Amount = decimal.Parse(response.txnAmount),
                Currency = "INR",
                TransactionId = response.txnId
            };

            _logger.LogInformation("Paytm payment status retrieved. TxnId: {TxnId}, Status: {Status}",
                responseObj.TransactionId, responseObj.Status);

            return responseObj;
        }

        public override Task<bool> ValidateWebhookSignatureAsync(string payload, string signature)
        {
            _logger.LogInformation("Validating Paytm webhook signature");

            try
            {
                var expectedSignature = _securityService.ComputeHmacSha256(payload, _options.WebhookSecret);
                return Task.FromResult(signature == expectedSignature);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating Paytm webhook signature");
                return Task.FromResult(false);
            }
        }

        public override WebhookEvent ParseWebhookEvent(string payload)
        {
            try
            {
                var jsonData = JsonSerializer.Deserialize<JsonElement>(payload);
                var orderId = jsonData.GetProperty("orderId").GetString();
                var txnId = jsonData.GetProperty("txnId").GetString();
                var status = jsonData.GetProperty("resultInfo").GetProperty("resultStatus").GetString();
                var amount = decimal.Parse(jsonData.GetProperty("txnAmount").GetString());

                return new WebhookEvent
                {
                    EventType = "transaction.status",
                    PaymentId = txnId,
                    OrderId = orderId,
                    Status = MapStatus(status),
                    Amount = amount,
                    Currency = "INR",
                    Timestamp = DateTime.UtcNow
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error parsing Paytm webhook event");
                throw new PaymentGatewayException("Error parsing webhook", ex.Message);
            }
        }

        private PaymentStatus MapStatus(string providerStatus)
        {
            return providerStatus.ToUpper() switch
            {
                "TXN_SUCCESS" => PaymentStatus.Captured,
                "PENDING" => PaymentStatus.Pending,
                "TXN_FAILURE" => PaymentStatus.Failed,
                _ => PaymentStatus.Pending
            };
        }
    }
}
