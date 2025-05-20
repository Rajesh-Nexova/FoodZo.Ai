using FoodZOAI.PaymentGateway.Configuration;
using FoodZOAI.PaymentGateway.Factory;
using FoodZOAI.PaymentGateway.Security.Services;
using FoodZOAI.PaymentGateway.Services;

namespace FoodZOAI.PaymentGateway.Utils
{
    public static class PaymentGatewayServiceExtensions
    {
        public static IServiceCollection AddPaymentGateway(this IServiceCollection services, IConfiguration configuration)
        {
            // Register options
            services.Configure<PaymentGatewayOptions>(configuration.GetSection("PaymentGateway"));

            // Register HTTP client for payment gateways
            services.AddHttpClient<RazorPayGatewayService>();
            services.AddHttpClient<PaytmGatewayService>();
            services.AddHttpClient<UpiPaymentService>();

            // Register services
            services.AddScoped<ISecurityService, SecurityService>();
            services.AddScoped<IPaymentGatewayFactory, PaymentGatewayFactory>();
            services.AddScoped<IUnifiedPaymentService, UnifiedPaymentService>();

            // Register specific gateway services
            services.AddScoped<RazorPayGatewayService>();
            services.AddScoped<PaytmGatewayService>();
            services.AddScoped<UpiPaymentService>();

            return services;
        }
    }
}
