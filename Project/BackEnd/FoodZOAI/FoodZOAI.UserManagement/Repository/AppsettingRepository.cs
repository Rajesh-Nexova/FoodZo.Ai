using FoodZOAI.UserManagement.Contracts;
using FoodZOAI.UserManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodZOAI.UserManagement.Repository
{
    public class AppsettingRepository:IAppsettingRepository
    {
        private readonly FoodZoaiContext _context;

        public AppsettingRepository(FoodZoaiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Appsetting>> GetAllAsync()
        {
            return await _context.Appsettings.ToListAsync();
        }

        public async Task<Appsetting?> GetByIdAsync(int id)
        {
            return await _context.Appsettings.FindAsync(id);
        }

        public async Task<Appsetting?> GetByKeyAsync(string key)
        {
            return await _context.Appsettings.FirstOrDefaultAsync(a => a.Key == key);
        }
        public async Task<Appsetting> AddAsync(Appsetting setting)
        {
            _context.Appsettings.Add(setting);
            await _context.SaveChangesAsync();
            return setting;
        }

        public async Task<Appsetting> UpdateAsync(int id, Appsetting updatedSetting)
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
        public async Task<bool> DeleteAsync(int id)
        {
            var setting = await _context.Appsettings.FindAsync(id);
            if (setting == null) return false;

            _context.Appsettings.Remove(setting);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
