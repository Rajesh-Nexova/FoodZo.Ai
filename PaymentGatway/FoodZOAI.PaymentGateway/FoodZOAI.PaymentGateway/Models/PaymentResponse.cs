namespace FoodZOAI.PaymentGateway.Models
{
    public class PaymentResponse
    {
        public string PaymentId { get; set; }
        public string OrderId { get; set; }
        public PaymentStatus Status { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string RedirectUrl { get; set; }
        public string TransactionId { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public Dictionary<string, string> MetaData { get; set; } = new Dictionary<string, string>();
    }
}
