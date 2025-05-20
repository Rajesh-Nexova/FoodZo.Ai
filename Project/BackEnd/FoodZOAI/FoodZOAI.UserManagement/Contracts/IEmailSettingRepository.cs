using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement.Contracts
{
    public interface IEmailSettingRepository
    {
        Task<IEnumerable<EmailSetting>> GetAllAsync();
        Task<EmailSetting?> GetByIdAsync(int id);
        Task AddAsync(EmailSetting emailSetting);
        Task UpdateAsync(EmailSetting emailSetting);
        Task DeleteAsync(int id);
        
        Task<bool> ExistsAsync(int id);

        
        
    }
}
