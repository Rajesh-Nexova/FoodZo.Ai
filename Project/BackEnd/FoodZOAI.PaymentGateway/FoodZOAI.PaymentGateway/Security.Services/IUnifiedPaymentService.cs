using FoodZOAI.PaymentGateway.Models;

namespace FoodZOAI.PaymentGateway.Security.Services
{
    public interface IUnifiedPaymentService
    {
        Task<PaymentResponse> ProcessPaymentAsync(PaymentRequest request);
        Task<PaymentResponse> CapturePaymentAsync(string paymentId, decimal amount, string provider = null);
        Task<PaymentResponse> RefundPaymentAsync(string paymentId, decimal amount, string reason = null, string provider = null);
        Task<PaymentResponse> GetPaymentStatusAsync(string paymentId, string provider = null);
        Task<bool> ValidateWebhookAsync(string payload, string signature, string provider);
        WebhookEvent ParseWebhookEvent(string payload, string provider);
    }
}

