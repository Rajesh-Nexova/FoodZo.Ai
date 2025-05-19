namespace FoodZOAI.PaymentGateway.Models
{
    public class CardDetails
    {
        public string CardNumber { get; set; }
        public string ExpiryMonth { get; set; }
        public string ExpiryYear { get; set; }
        public string Cvv { get; set; }
        public string CardHolderName { get; set; }
    }
}
