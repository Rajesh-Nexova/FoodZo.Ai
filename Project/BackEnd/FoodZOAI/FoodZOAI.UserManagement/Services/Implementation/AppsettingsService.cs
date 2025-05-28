using FoodZOAI.UserManagement.Configuration.Contracts;
using FoodZOAI.UserManagement.Contracts;
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Repository;
using FoodZOAI.UserManagement.Services.Contract;
using ILogger = Serilog.ILogger;


namespace FoodZOAI.UserManagement.Services.Implementation
{
	/// <summary>
	/// 
	/// </summary>
	public class AppsettingsService : IAppsettingsService
	{
		private readonly IAppsettingRepository _appsettingRepository;
		private readonly IAppsettingMapper _appsettingMapper;
		private readonly ILogger<AppsettingsService> _logger;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="appsettingRepository"></param>
		/// <param name="appsettingMapper"></param>
		/// <param name="logger"></param>
		public AppsettingsService(IAppsettingRepository appsettingRepository,
			IAppsettingMapper appsettingMapper,
			ILogger<AppsettingsService> logger)
		{
			_appsettingRepository = appsettingRepository;
			_appsettingMapper = appsettingMapper;
			_logger = logger;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="appsetting"></param>
		/// <returns></returns>
		public async Task<AppsettingDTO> AddAppSettingAsync(AppsettingDTO appsetting)
		{
			try
			{
				var entity =  await _appsettingRepository
					.AddAsync(_appsettingMapper.MapToDomain(appsetting));

				return _appsettingMapper.Map(entity); 
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error While Adding Appsettings");
				throw;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public async Task<bool> DeleteAppSettingAsync(int id)
		{
			try 
			{
				return await _appsettingRepository.DeleteAsync(id);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error While DeleteAppSetting");
				throw;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public async Task<List<AppsettingDTO>> GetAllAppsettingsAsync()
		{
			try
			{ 
				var appSettings = await _appsettingRepository.GetAllAsync();
				 
				return _appsettingMapper.MapList(appSettings);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error While GetAllAppsettingsAsync");
				throw;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public Task<AppsettingDTO> GetAppsettingByIdAsync(int id)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public Task<AppsettingDTO> GetAppsettingByKeyAsync(string key)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="appsetting"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public Task<AppsettingDTO> UpdateAppSettingAsync(AppsettingDTO appsetting)
		{
			throw new NotImplementedException();
		}
	}
}
