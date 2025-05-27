using FoodZOAI.UserManagement.Contracts;
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodZOAI.UserManagement.Repository
{
    public class EmailSettingRepository : IEmailSettingRepository
    {
        public readonly FoodZoaiContext _Context;
        public EmailSettingRepository(FoodZoaiContext Context)
        {
            _Context = Context;
        }

        public  async Task<EmailSetting?> GetByIdEmail(int id)
        {
            return  await _Context.EmailSettings.FirstOrDefaultAsync(e => e.Id == id && e.IsActive);
        }

        public  async Task<EmailSetting?> GetDefaultActiveAsync()
        {
            return await _Context.EmailSettings.FirstOrDefaultAsync(e => e.IsDefault && e.IsActive);
        }

        public async Task AddAsync(EmailSetting emailSetting)
        {
            _Context.EmailSettings.Add(emailSetting);
            await _Context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var email = await _Context.EmailSettings.FindAsync(id);
            if (email != null)
            {
                _Context.EmailSettings.Remove(email);
                await _Context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _Context.EmailSettings.AnyAsync(e => e.Id == id);
        }

        

        public async Task<IEnumerable<EmailSetting>> GetAllAsync()
        {
            return await _Context.EmailSettings.ToListAsync();
        }

        public async Task<EmailSetting?> GetByIdAsync(int id)
        {
            return await _Context.EmailSettings.FindAsync(id);
        }

        

        public async Task UpdateAsync(EmailSetting emailSetting)
        {
            _Context.EmailSettings.Update(emailSetting);
            await _Context.SaveChangesAsync();
        }
    }
}
