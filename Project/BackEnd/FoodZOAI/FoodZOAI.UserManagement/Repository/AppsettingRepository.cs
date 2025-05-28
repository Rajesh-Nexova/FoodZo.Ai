using FoodZOAI.UserManagement.Contracts;
using FoodZOAI.UserManagement.Models;
using Microsoft.EntityFrameworkCore;
using ILogger = Serilog.ILogger;

namespace FoodZOAI.UserManagement.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class AppsettingRepository:IAppsettingRepository
    {
        private readonly FoodZoaiContext _context;
		private readonly ILogger<AppsettingRepository> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
		public AppsettingRepository(FoodZoaiContext context,
            ILogger<AppsettingRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<Appsetting>> GetAllAsync()
        {
            try
            {
                return await _context.Appsettings.ToListAsync();
            }
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error retrieving all AppSettings");
				throw;
			}
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Appsetting?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Appsettings.FindAsync(id);
            }
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error retrieving getting AppSetting by id");
				throw;
			}
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<Appsetting?> GetByKeyAsync(string key)
        {
            try
            {
                return await _context.Appsettings.
                    FirstOrDefaultAsync(a => a.Key == key);
            }
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error retrieving  AppSettings by Key");
				throw;
			}
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public async Task<Appsetting> AddAsync(Appsetting setting)
        {
            // Santhosh - Need to check the State 

            try
            {
                _context.Appsettings.Add(setting);
                await _context.SaveChangesAsync();
                return setting;
            }
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error while adding AppSettings");
				throw;
			}
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedSetting"></param>
        /// <returns></returns>
        public async Task<Appsetting> UpdateAsync(int id, Appsetting updatedSetting)
        {
            // Santhosh - Need to check the State 
            try
            {
                var setting = await _context.Appsettings.FindAsync(id);
                if (setting == null) return null!;

                setting.Name = updatedSetting.Name;
                setting.Key = updatedSetting.Key;
                setting.Value = updatedSetting.Value;
                setting.ModifiedByUser = updatedSetting.ModifiedByUser;
                setting.IsActive = updatedSetting.IsActive;

                await _context.SaveChangesAsync();
                return setting;
            }
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error while Updating AppSettings");
				throw;
			}
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                // Santosh this has to Soft Delete 
                var setting = await _context.Appsettings.FindAsync(id);
                if (setting == null) return false;

                _context.Appsettings.Remove(setting);
                await _context.SaveChangesAsync();
                return true;
            }
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error while deleting AppSettings");
				throw;
			}
		}
    }
}
