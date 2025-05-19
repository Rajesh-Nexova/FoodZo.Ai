using FoodZOAI.PaymentGateway.CustomException;
using FoodZOAI.PaymentGateway.Factory;
using FoodZOAI.PaymentGateway.Models;

namespace FoodZOAI.PaymentGateway.Security.Services
{
    public class UnifiedPaymentService : IUnifiedPaymentService
    {
        private readonly IPaymentGatewayFactory _gatewayFactory;
        private readonly ILogger<UnifiedPaymentService> _logger;
        private readonly ISecurityService _securityService;

        public UnifiedPaymentService(
            IPaymentGatewayFactory gatewayFactory,
            ILogger<UnifiedPaymentService> logger,
            ISecurityService securityService)
        {
            _gatewayFactory = gatewayFactory ?? throw new ArgumentNullException(nameof(gatewayFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _securityService = securityService ?? throw new ArgumentNullException(nameof(securityService));
        }

        public async Task<PaymentResponse> ProcessPaymentAsync(PaymentRequest request)
        {
            _logger.LogInformation("Processing payment for order {OrderId} using method {PaymentMethod}",
                request.OrderId, request.PaymentMethod);

            // Validate request
            ValidatePaymentRequest(request);

            // Generate order ID if not provided
            if (string.IsNullOrWhiteSpace(request.OrderId))
            {
                request.OrderId = _securityService.GenerateOrderId();
            }

            // Get appropriate gateway based on payment method
            var gateway = _gatewayFactory.CreateGateway(request.PaymentMethod);

            try
            {
                var response = await gateway.InitiatePaymentAsync(request);

                _logger.LogInformation("Payment processed successfully. Order ID: {OrderId}, Payment ID: {PaymentId}",
                    request.OrderId, response.PaymentId);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing payment for order {OrderId}", request.OrderId);
                throw;
            }
        }

        public async Task<PaymentResponse> CapturePaymentAsync(string paymentId, decimal amount, string provider = null)
        {
            _logger.LogInformation("Capturing payment {PaymentId} for amount {Amount}", paymentId, amount);

            var gateway = string.IsNullOrEmpty(provider)
                ? _gatewayFactory.CreateGateway("razorpay") // Default to RazorPay
                : _gatewayFactory.CreateGateway(provider);

            try
            {
                var response = await gateway.CapturePaymentAsync(paymentId, amount);

                _logger.LogInformation("Payment captured successfully. Payment ID: {PaymentId}", paymentId);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error capturing payment {PaymentId}", paymentId);
                throw;
            }
        }

        public async Task<PaymentResponse> RefundPaymentAsync(string paymentId, decimal amount, string reason = null, string provider = null)
        {
            _logger.LogInformation("Refunding payment {PaymentId} for amount {Amount}", paymentId, amount);

            var gateway = string.IsNullOrEmpty(provider)
                ? _gatewayFactory.CreateGateway("razorpay") // Default to RazorPay
                : _gatewayFactory.CreateGateway(provider);

            try
            {
                var response = await gateway.RefundPaymentAsync(paymentId, amount, reason);

                _logger.LogInformation("Payment refunded successfully. Payment ID: {PaymentId}", paymentId);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error refunding payment {PaymentId}", paymentId);
                throw;
            }
        }

        public async Task<PaymentResponse> GetPaymentStatusAsync(string paymentId, string provider = null)
        {
            _logger.LogInformation("Getting payment status for {PaymentId}", paymentId);

            var gateway = string.IsNullOrEmpty(provider)
                ? _gatewayFactory.CreateGateway("razorpay") // Default to RazorPay
                : _gatewayFactory.CreateGateway(provider);

            try
            {
                var response = await gateway.GetPaymentStatusAsync(paymentId);

                _logger.LogInformation("Payment status retrieved. Payment ID: {PaymentId}, Status: {Status}",
                    paymentId, response.Status);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting payment status for {PaymentId}", paymentId);
                throw;
            }
        }

        public async Task<bool> ValidateWebhookAsync(string payload, string signature, string provider)
        {
            _logger.LogInformation("Validating webhook for provider {Provider}", provider);

            var gateway = _gatewayFactory.CreateGateway(provider);

            try
            {
                var isValid = await gateway.ValidateWebhookSignatureAsync(payload, signature);

                _logger.LogInformation("Webhook validation result: {IsValid}", isValid);

                return isValid;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating webhook for provider {Provider}", provider);
                return false;
            }
        }

        public WebhookEvent ParseWebhookEvent(string payload, string provider)
        {
            _logger.LogInformation("Parsing webhook event for provider {Provider}", provider);

            var gateway = _gatewayFactory.CreateGateway(provider);

            try
            {
                var webhookEvent = gateway.ParseWebhookEvent(payload);

                _logger.LogInformation("Webhook event parsed. Event Type: {EventType}, Payment ID: {PaymentId}",
                    webhookEvent.EventType, webhookEvent.PaymentId);

                return webhookEvent;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error parsing webhook event for provider {Provider}", provider);
                throw;
            }
        }

        private void ValidatePaymentRequest(PaymentRequest request)
        {
            if (request == null)
                throw new PaymentGatewayException("Payment request cannot be null");

            if (request.Amount <= 0)
                throw new PaymentGatewayException("Payment amount must be greater than zero");

            if (string.IsNullOrWhiteSpace(request.Currency))
                request.Currency = "INR";

            if (string.IsNullOrWhiteSpace(request.CustomerEmail))
                throw new PaymentGatewayException("Customer email is required");

            if (string.IsNullOrWhiteSpace(request.CustomerPhone))
                throw new PaymentGatewayException("Customer phone is required");

            // Validate phone number format (simple validation)
            if (!request.CustomerPhone.All(c => char.IsDigit(c) || c == '+' || c == '-' || c == ' '))
                throw new PaymentGatewayException("Invalid phone number format");

            // Validate email format
            try
            {
                var addr = new System.Net.Mail.MailAddress(request.CustomerEmail);
                if (addr.Address != request.CustomerEmail)
                    throw new PaymentGatewayException("Invalid email format");
            }
            catch
            {
                throw new PaymentGatewayException("Invalid email format");
            }

            // Validate UPI ID if UPI payment method
            if (request.PaymentMethod == PaymentMethod.Upi && request.MetaData.ContainsKey("vpa"))
            {
                if (!_securityService.ValidateUpiId(request.MetaData["vpa"]))
                    throw new PaymentGatewayException("Invalid UPI ID format");
            }
        }
    }
}
