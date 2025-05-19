namespace FoodZOAI.PaymentGateway.Security.Services
{
    public interface ISecurityService
    {
        string ComputeHmacSha256(string data, string key);
        string EncryptPan(string panNumber);
        string DecryptPan(string encryptedPan);
        bool ValidateCardNumber(string cardNumber);
        bool ValidateExpiryDate(string month, string year);
        bool ValidateCvv(string cvv);
        bool ValidateUpiId(string upiId);
        string MaskCardNumber(string cardNumber);
        string GenerateOrderId();
    }
}
