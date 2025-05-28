using FoodZOAI.FileSystem.Core.Contract;
using FoodZOAI.FileSystem.Models.Config;
using FoodZOAI.FileSystem.Providers;

namespace FoodZOAI.UserManagement.FileFactory
{
	public class FileStorageFactory : IFileStorageFactory
	{
		private readonly StorageConfiguration _config;
		private readonly IServiceProvider _serviceProvider;
		private readonly ILogger<FileStorageFactory> _logger;

		public FileStorageFactory(StorageConfiguration config, IServiceProvider serviceProvider, ILogger<FileStorageFactory> logger)
		{
			_config = config;
			_serviceProvider = serviceProvider;
			_logger = logger;
		}

		public IFileStorageService CreateStorageService()
		{
			return _config.Provider.ToUpperInvariant() switch
			{
				"LOCAL" => CreateLocalStorageService(),
				"AZURE" => CreateAzureStorageService(),
				"AWS" => CreateAwsStorageService(),
				_ => throw new NotSupportedException($"Storage provider '{_config.Provider}' is not supported")
			};
		}

		private IFileStorageService CreateLocalStorageService()
		{
			if (_config.Local == null)
				throw new InvalidOperationException("Local storage configuration is missing");

			var logger = _serviceProvider.GetRequiredService<ILogger<LocalFileStorageProvider>>();
			return new LocalFileStorageProvider(_config.Local, logger);
		}

		private IFileStorageService CreateAzureStorageService()
		{
			if (_config.Azure == null)
				throw new InvalidOperationException("Azure storage configuration is missing");

			var logger = _serviceProvider.GetRequiredService<ILogger<AzureFileStorageProvider>>();
			return new AzureFileStorageProvider(_config.Azure, logger);
		}

		private IFileStorageService CreateAwsStorageService()
		{
			if (_config.Aws == null)
				throw new InvalidOperationException("AWS storage configuration is missing");

			var logger = _serviceProvider.GetRequiredService<ILogger<AwsFileStorageProvider>>();
			return new AwsFileStorageProvider(_config.Aws, logger);
		}
	}
}
