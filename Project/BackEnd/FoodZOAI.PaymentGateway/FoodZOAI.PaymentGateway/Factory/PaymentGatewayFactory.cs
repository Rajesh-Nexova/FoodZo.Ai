using FoodZOAI.PaymentGateway.CustomException;
using FoodZOAI.PaymentGateway.Models;
using FoodZOAI.PaymentGateway.Services;

namespace FoodZOAI.PaymentGateway.Factory
{
    public class PaymentGatewayFactory : IPaymentGatewayFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<PaymentGatewayFactory> _logger;

        public PaymentGatewayFactory(IServiceProvider serviceProvider, ILogger<PaymentGatewayFactory> logger)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IPaymentGatewayService CreateGateway(string providerName)
        {
            _logger.LogInformation("Creating payment gateway for provider: {ProviderName}", providerName);

            return providerName.ToLower() switch
            {
                "razorpay" => _serviceProvider.GetRequiredService<RazorPayGatewayService>(),
                "paytm" => _serviceProvider.GetRequiredService<PaytmGatewayService>(),
                "upi" => _serviceProvider.GetRequiredService<UpiPaymentService>(),
                _ => throw new PaymentGatewayException($"Unsupported payment provider: {providerName}")
            };
        }

        public IPaymentGatewayService CreateGateway(PaymentMethod paymentMethod)
        {
            _logger.LogInformation("Creating payment gateway for method: {PaymentMethod}", paymentMethod);

            return paymentMethod switch
            {
                PaymentMethod.Upi => _serviceProvider.GetRequiredService<UpiPaymentService>(),
                PaymentMethod.CreditCard or PaymentMethod.DebitCard or PaymentMethod.NetBanking or PaymentMethod.Wallet
                    => _serviceProvider.GetRequiredService<RazorPayGatewayService>(),
                _ => throw new PaymentGatewayException($"Unsupported payment method: {paymentMethod}")
            };
        }
    }
}
