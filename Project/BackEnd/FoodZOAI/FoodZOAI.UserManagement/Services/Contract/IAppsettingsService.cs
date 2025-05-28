using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement.Services.Contract
{
	public interface IAppsettingsService
	{
		public Task<List<AppsettingDTO>> GetAllAppsettingsAsync();
		public Task<AppsettingDTO> GetAppsettingByKeyAsync(string key);
		public Task<AppsettingDTO> GetAppsettingByIdAsync(int id);
		public Task<AppsettingDTO> AddAppSettingAsync(AppsettingDTO appsetting);
		public Task<AppsettingDTO> UpdateAppSettingAsync(AppsettingDTO appsetting);
		public Task<bool> DeleteAppSettingAsync(int id);

	}
}
