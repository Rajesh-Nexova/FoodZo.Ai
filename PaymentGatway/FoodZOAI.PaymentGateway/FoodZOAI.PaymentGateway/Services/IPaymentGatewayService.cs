using FoodZOAI.PaymentGateway.Models;

namespace FoodZOAI.PaymentGateway.Services
{
    public interface IPaymentGatewayService
    {
        Task<PaymentResponse> InitiatePaymentAsync(PaymentRequest request);
        Task<PaymentResponse> CapturePaymentAsync(string paymentId, decimal amount);
        Task<PaymentResponse> RefundPaymentAsync(string paymentId, decimal amount, string reason = null);
        Task<PaymentResponse> GetPaymentStatusAsync(string paymentId);
        Task<bool> ValidateWebhookSignatureAsync(string payload, string signature);
        WebhookEvent ParseWebhookEvent(string payload);
    }
}
