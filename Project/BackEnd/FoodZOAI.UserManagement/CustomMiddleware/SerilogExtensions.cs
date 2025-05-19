using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using Serilog.Filters;
using Serilog.Sinks.File;                
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FoodZOAI.UserManagement.CustomMiddleware
{
    public static class SerilogExtensions
    {
        public static WebApplicationBuilder AddSerilogServices(this WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog((context, services, configuration) =>
            {
                configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services)
                    .Enrich.FromLogContext()
                    .Enrich.WithMachineName()
                    .Enrich.WithEnvironmentName()
                    .WriteTo.Console(new CompactJsonFormatter())
                    .WriteTo.File(new CompactJsonFormatter(),
                        path: Path.Combine("logs", "log-.json"),
                        rollingInterval: RollingInterval.Day,          
                        retainedFileCountLimit: 30)
                    .WriteTo.Seq("http://localhost:5341")
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
