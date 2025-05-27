using FoodZOAI.UserManagement.DTOs;

namespace FoodZOAI.UserManagement.Services.Contract
{
    public interface IEmailSMTPSettingService
    {
        Task<List<EmailSettingDTO>> GetAllEmailSMTPSettingAsync();
        Task<EmailSettingDTO?> GetEmailSMTPSettingByIdAsync(int id);
        Task<EmailSettingDTO> AddEmailSMTPSettingAsync(EmailSettingDTO dto);
        Task<EmailSettingDTO?> UpdateEmailSMTPSettingAsync(int id, EmailSettingDTO dto);
        Task<bool> DeleteEmailSMTPSettingAsync(int id);
    }
}
