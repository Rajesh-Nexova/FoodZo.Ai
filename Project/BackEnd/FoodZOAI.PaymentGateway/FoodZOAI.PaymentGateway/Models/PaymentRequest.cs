namespace FoodZOAI.PaymentGateway.Models
{
    public class PaymentRequest
    {
        public string OrderId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "INR";
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public string Description { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string RedirectUrl { get; set; }
        public string WebhookUrl { get; set; }
        public Dictionary<string, string> MetaData { get; set; } = new Dictionary<string, string>();
    }
}
