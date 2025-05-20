using FoodZOAI.UserManagement.Configuration.Contracts;
using FoodZOAI.UserManagement.Contracts;
using FoodZOAI.UserManagement.Repository;

namespace FoodZOAI.UserManagement.Configuration.Mappers
{
    public  static class MapperServiceExtension
    {
        public static IServiceCollection AddMappers(this IServiceCollection services)
        {
            services.AddScoped<IEmailSettingMapper, EmailSettingMapper>();
            services.AddScoped<IEmailTemplateMapper, EmailTemplateMapper>();
            
            return services;
        }

        public static IServiceCollection AddConfigServices(this IServiceCollection services)
        {
            services.AddScoped<IEmailSettingRepository, EmailSettingRepository>();
            services.AddScoped<IEmailTemplateRepository, EmailTemplateRepository>();
           
            return services;

        }
    }
}
