using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement.Contracts
{
    public interface IEmailSettingRepository
    {
        //Task<EmailSetting?> GetEmailSettingAsync(int? settingId);
        Task<EmailSetting?> GetByIdEmail(int id);
        Task<EmailSetting?> GetDefaultActiveAsync();


        Task<IEnumerable<EmailSetting>> GetAllAsync();
        Task<EmailSetting?> GetByIdAsync(int id);
        Task AddAsync(EmailSetting emailSetting);
        Task UpdateAsync(EmailSetting emailSetting);
        Task DeleteAsync(int id);
        
        Task<bool> ExistsAsync(int id);

        
        
    }
}
