using FoodZOAI.FileSystem.Core.Contract;
using FoodZOAI.FileSystem.Models.Config;
using FoodZOAI.UserManagement.Configuration.Contracts;
using FoodZOAI.UserManagement.Configuration.Mappers;
using FoodZOAI.UserManagement.Contracts;
using FoodZOAI.UserManagement.FileFactory;
using FoodZOAI.UserManagement.Repository;
using FoodZOAI.UserManagement.Services.Contract;
using FoodZOAI.UserManagement.Services.Implementation;

namespace FoodZOAI.UserManagement.Configuration
{
	public static class MapperServiceExtensions
	{
		public static IServiceCollection AddMappers(this IServiceCollection services)
		{
			services.AddScoped<IAppsettingMapper, AppsettingMapper>();
			return services;
		}

		public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
		{
			services.AddScoped<IAppsettingRepository, AppsettingRepository>();
			return services;
		}
		public static IServiceCollection AddConfigServices(this IServiceCollection services)
		{
			services.AddScoped<IAppsettingsService, AppsettingsService>();
			
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

			return services;
		}

	}
}
