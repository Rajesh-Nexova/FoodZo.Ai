namespace FoodZOAI.PaymentGateway.Models
{
    public class WebhookEvent
    {
        public string EventType { get; set; }  // payment.success, payment.failed, etc.
        public string PaymentId { get; set; }
        public string OrderId { get; set; }
        public PaymentStatus Status { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string TransactionId { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime Timestamp { get; set; }
        public Dictionary<string, string> MetaData { get; set; } = new Dictionary<string, string>();
    }
}
