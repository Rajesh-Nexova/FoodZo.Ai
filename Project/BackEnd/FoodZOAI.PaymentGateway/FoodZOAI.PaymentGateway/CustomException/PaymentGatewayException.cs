

namespace FoodZOAI.PaymentGateway.CustomException
{
    public class PaymentGatewayException : Exception
    {
        public string ErrorCode { get; }
        public string ProviderMessage { get; }

        public PaymentGatewayException(string message) : base(message)
        {
        }

        public PaymentGatewayException(string message, string providerMessage) : base(message)
        {
            ProviderMessage = providerMessage;
        }

        public PaymentGatewayException(string message, string providerMessage, Exception innerException)
            : base(message, innerException)
        {
            ProviderMessage = providerMessage;
        }

        public PaymentGatewayException(string errorCode, string message, string providerMessage) : base(message)
        {
            ErrorCode = errorCode;
            ProviderMessage = providerMessage;
        }
    }
}
