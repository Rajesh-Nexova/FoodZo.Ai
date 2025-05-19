using System.Security.Cryptography;
using System.Security;
using System.Text;

namespace FoodZOAI.PaymentGateway.Security.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly ILogger<SecurityService> _logger;
        private readonly string _encryptionKey;

        public SecurityService(ILogger<SecurityService> logger, IConfiguration configuration)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _encryptionKey = configuration["PaymentGateway:EncryptionKey"] ??
                throw new ArgumentNullException("EncryptionKey not configured");
        }

        public string ComputeHmacSha256(string data, string key)
        {
            try
            {
                using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key));
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
                return Convert.ToHexString(hash).ToLower();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error computing HMAC SHA256");
                throw new SecurityException("Error computing signature", ex);
            }
        }

        public string EncryptPan(string panNumber)
        {
            try
            {
                using var aes = Aes.Create();
                aes.Key = Encoding.UTF8.GetBytes(_encryptionKey.PadRight(32).Substring(0, 32));
                aes.IV = new byte[16]; // Use zero IV for simplicity, use random IV in production

                using var encryptor = aes.CreateEncryptor();
                var panBytes = Encoding.UTF8.GetBytes(panNumber);
                var encryptedBytes = encryptor.TransformFinalBlock(panBytes, 0, panBytes.Length);
                return Convert.ToBase64String(encryptedBytes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error encrypting PAN");
                throw new SecurityException("Error encrypting PAN", ex);
            }
        }

        public string DecryptPan(string encryptedPan)
        {
            try
            {
                using var aes = Aes.Create();
                aes.Key = Encoding.UTF8.GetBytes(_encryptionKey.PadRight(32).Substring(0, 32));
                aes.IV = new byte[16]; // Use zero IV for simplicity, use random IV in production

                using var decryptor = aes.CreateDecryptor();
                var encryptedBytes = Convert.FromBase64String(encryptedPan);
                var decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
                return Encoding.UTF8.GetString(decryptedBytes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error decrypting PAN");
                throw new SecurityException("Error decrypting PAN", ex);
            }
        }

        public bool ValidateCardNumber(string cardNumber)
        {
            if (string.IsNullOrWhiteSpace(cardNumber))
                return false;

            // Remove spaces and hyphens
            cardNumber = cardNumber.Replace(" ", "").Replace("-", "");

            // Check if all characters are digits
            if (!cardNumber.All(char.IsDigit))
                return false;

            // Check length (13-19 digits for most cards)
            if (cardNumber.Length < 13 || cardNumber.Length > 19)
                return false;

            // Luhn algorithm validation
            return ValidateLuhnAlgorithm(cardNumber);
        }

        private bool ValidateLuhnAlgorithm(string cardNumber)
        {
            int sum = 0;
            bool alternate = false;

            for (int i = cardNumber.Length - 1; i >= 0; i--)
            {
                int digit = int.Parse(cardNumber[i].ToString());

                if (alternate)
                {
                    digit *= 2;
                    if (digit > 9)
                        digit = (digit % 10) + 1;
                }

                sum += digit;
                alternate = !alternate;
            }

            return sum % 10 == 0;
        }

        public bool ValidateExpiryDate(string month, string year)
        {
            if (!int.TryParse(month, out int monthInt) || !int.TryParse(year, out int yearInt))
                return false;

            if (monthInt < 1 || monthInt > 12)
                return false;

            // Handle 2-digit year
            if (yearInt < 100)
                yearInt += 2000;

            var expiryDate = new DateTime(yearInt, monthInt, DateTime.DaysInMonth(yearInt, monthInt));
            return expiryDate >= DateTime.Now.Date;
        }

        public bool ValidateCvv(string cvv)
        {
            if (string.IsNullOrWhiteSpace(cvv))
                return false;

            // CVV should be 3 or 4 digits
            return cvv.Length >= 3 && cvv.Length <= 4 && cvv.All(char.IsDigit);
        }

        public bool ValidateUpiId(string upiId)
        {
            if (string.IsNullOrWhiteSpace(upiId))
                return false;

            // Basic UPI ID validation: should contain @ and have valid format
            var parts = upiId.Split('@');
            if (parts.Length != 2)
                return false;

            var username = parts[0];
            var handle = parts[1];

            // Username should be alphanumeric and at least 3 characters
            if (username.Length < 3 || !username.All(c => char.IsLetterOrDigit(c) || c == '.' || c == '_'))
                return false;

            // Handle should be valid (e.g., paytm, phonepe, googlepay, etc.)
            var validHandles = new[] { "paytm", "phonepe", "googlepay", "okaxis", "ybl", "ibl", "oksbi", "okhdfcbank" };
            return validHandles.Contains(handle.ToLower());
        }

        public string MaskCardNumber(string cardNumber)
        {
            if (string.IsNullOrWhiteSpace(cardNumber) || cardNumber.Length < 4)
                return cardNumber;

            // Show first 4 and last 4 digits, mask the rest
            var cleanNumber = cardNumber.Replace(" ", "").Replace("-", "");
            if (cleanNumber.Length <= 8)
                return cleanNumber;

            var first4 = cleanNumber.Substring(0, 4);
            var last4 = cleanNumber.Substring(cleanNumber.Length - 4);
            var masked = new string('*', cleanNumber.Length - 8);

            return $"{first4}{masked}{last4}";
        }

        public string GenerateOrderId()
        {
            return $"ORD_{DateTime.UtcNow:yyyyMMddHHmmss}_{Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6).ToUpper()}";
        }
    }
}
