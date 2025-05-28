using FoodZOAI.FileSystem.Core.Contract;
using FoodZOAI.FileSystem.Models.Config;
using FoodZOAI.UserManagement.Configuration.Contracts;
using FoodZOAI.UserManagement.Configuration.Mappers;
using FoodZOAI.UserManagement.Contracts;
using FoodZOAI.UserManagement.FileFactory;
using FoodZOAI.UserManagement.Mappers.Implementations;
using FoodZOAI.UserManagement.Mappers.Interfaces;
using FoodZOAI.UserManagement.Repositories;
using FoodZOAI.UserManagement.Repositories.Contracts;
using FoodZOAI.UserManagement.Repositories.Implementations;
using FoodZOAI.UserManagement.Repositories.Interfaces;
using FoodZOAI.UserManagement.Repository;
using FoodZOAI.UserManagement.Services;
using FoodZOAI.UserManagement.Services.Contract;
using FoodZOAI.UserManagement.Services.Contracts;
using FoodZOAI.UserManagement.Services.Implementation;
using FoodZOAI.UserManagement.Services.Implementations;
using FoodZOAI.UserManagement.Services.Interfaces;

namespace FoodZOAI.UserManagement.Configuration
{
	public static class MapperServiceExtensions
	{
		/// <summary>
		/// Dependency injection configuration for add mappers
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddMappers(this IServiceCollection services)
		{
			services.AddScoped<IAppsettingMapper, AppsettingMapper>();
            services.AddScoped<IUserProfileMapper, UserProfileMapper>();
            services.AddScoped<IUserMapper, UserMapper>();
            services.AddScoped<IPermissionMapper, PermissionMapper>();
            services.AddScoped<IEmailSettingMapper, EmailSettingMapper>();
            services.AddScoped<IEmailTemplateMapper, EmailTemplateMapper>();

            services.AddScoped<IDailyReminderMapper, DailyReminderMapper>();
            services.AddScoped<IHalfYearlyReminderMapper, HalfYearlyReminderMapper>();
            services.AddScoped<IMonthlyReminderMapper, MonthlyReminderMapper>();
            services.AddScoped<IQuarterlyReminderMapper, QuarterlyReminderMapper>();
            services.AddScoped<IWeeklyReminderMapper, WeeklyReminderMapper>();
            services.AddScoped<IYearlyReminderMapper, YearlyReminderMapper>();
            services.AddScoped<IOneTimeReminderMapper, OneTimeReminderMapper>();




            services.AddScoped<IPermissionMapper, PermissionMapper>();

            services.AddScoped<IOrganizationMapper, OrganizationMapper>();




            return services;
		}












		public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
		{
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAppsettingRepository, AppsettingRepository>();


            services.AddScoped<IDailyReminderRepository, DailyReminderRepository>();
            services.AddScoped<IHalfYearlyReminderRepository, HalfYearlyReminderRepository>();
            services.AddScoped<IMonthlyReminderRepository, MonthlyReminderRepository>();
            services.AddScoped<IQuarterlyReminderRepository, QuarterlyReminderRepository>();
            services.AddScoped<IWeeklyReminderRepository, WeeklyReminderRepository>();
            services.AddScoped<IYearlyReminderRepository, YearlyReminderRepository>();
            services.AddScoped<IOneTimeReminderRepository, OneTimeReminderRepository>();
            return services;
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IAppsettingRepository, AppsettingRepository>();

			

			services.AddScoped<IAppsettingRepository, AppsettingRepository>();

            services.AddScoped<IEmailSettingRepository, EmailSettingRepository>();
            services.AddScoped<IEmailTemplateRepository, EmailTemplateRepository>();
            services.AddScoped<IOrganizationRepository, OrganizationRepository>();
            return services;
		}
		public static IServiceCollection AddConfigServices(this IServiceCollection services)
		{
			services.AddScoped<IAppsettingsService, AppsettingsService>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IFileService, FileService>();

            services.AddScoped<IEmailSMTPSettingService, EmailSMTPSettingService>();
            services.AddScoped<IEmailTemplateService, EmailTemplateService>();


            services.AddScoped<IDailyReminderService, DailyReminderService>();
            services.AddScoped<IHalfYearlyReminderService, HalfYearlyReminderService>();
            services.AddScoped<IMonthlyReminderService, MonthlyReminderService>();
            services.AddScoped<IQuarterlyReminderService, QuarterlyReminderService>();
            services.AddScoped<IWeeklyReminderService, WeeklyReminderService>();
            services.AddScoped<IYearlyReminderService, YearlyReminderService>();
            services.AddScoped<IOneTimeReminderService, OneTimeReminderService>();



            return services;
		}

		public static IServiceCollection AddFileStorage(this IServiceCollection services, IConfiguration configuration)
		{
			// Bind configuration
			var storageConfig = new StorageConfiguration();
			configuration.GetSection("Storage").Bind(storageConfig);
			services.AddSingleton(storageConfig);

			// Register factory and service
			services.AddSingleton<IFileStorageFactory, FileStorageFactory>();
			services.AddScoped<IFileStorageService>(provider =>
			{
				var factory = provider.GetRequiredService<IFileStorageFactory>();
				return factory.CreateStorageService();
			});
            services.AddScoped<IPermissionService, PermissionService>();
          services.AddScoped<IPermissionService, PermissionService>();
            return services;
		}

	}
}

