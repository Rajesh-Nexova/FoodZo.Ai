using Serilog;
using Serilog.Filters;
using Serilog.Formatting.Compact;

namespace FoodZOAI.PaymentGateway.Custom.Middleware
{
    public static class CustomMiddlewareExtensions
    {
        public static WebApplicationBuilder AddSerilogServices(this WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog((context, services, configuration) => {
                configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services)
                    .Enrich.FromLogContext()
                    .Enrich.WithMachineName()
                    .Enrich.WithEnvironmentName()
                    .WriteTo.Console(new CompactJsonFormatter())
                    .WriteTo.File(new CompactJsonFormatter(),
                        Path.Combine("logs", "log-.json"),
                        rollingInterval: RollingInterval.Day,
                        retainedFileCountLimit: 30)
                    .WriteTo.Seq("http://localhost:5341")  // Optional: Seq integration
                    .Filter.ByExcluding(Matching.FromSource("Microsoft.AspNetCore.StaticFiles"));
            });

            return builder;
        }

        public static IApplicationBuilder UseSerilogMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<SerilogMiddleware>();
        }
    }
}
