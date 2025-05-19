namespace FoodZOAI.PaymentGateway.Configuration
{
    public class PaymentGatewayOptions
    {
        public string ApiKey { get; set; }
        public string SecretKey { get; set; }
        public string MerchantId { get; set; }
        public string ApiBaseUrl { get; set; }
        public bool IsSandbox { get; set; } = true;
        public int TimeoutSeconds { get; set; } = 30;
        public string WebhookSecret { get; set; }
    }
}
