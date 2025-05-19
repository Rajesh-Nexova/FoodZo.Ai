namespace FoodZOAI.PaymentGateway.Models
{
    public class WalletDetails
    {
        public string WalletProvider { get; set; }  // GPAY, BHIM,PAYTM, PHONEPE, AMAZONPAY, etc.
        public string WalletId { get; set; }
    }
}
