using FoodZOAI.PaymentGateway.Models;
using FoodZOAI.PaymentGateway.Services;

namespace FoodZOAI.PaymentGateway.Factory
{
    public interface IPaymentGatewayFactory
    {
        IPaymentGatewayService CreateGateway(string providerName);
        IPaymentGatewayService CreateGateway(PaymentMethod paymentMethod);
    }

}
