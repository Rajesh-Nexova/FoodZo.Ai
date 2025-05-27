using FoodZOAI.UserManagement.DTOs;

namespace FoodZOAI.UserManagement.Services.Contract
{
    public interface IEmailTemplateService
    {
        Task<List<EmailTemplateDTO>> GetAllEmailTemplateAsync();
        Task<EmailTemplateDTO?> GetEmailTemplateByIdAsync(int id);
        Task<EmailTemplateDTO> AddEmailTemplateAsync(EmailTemplateDTO dto);
        Task<EmailTemplateDTO?> UpdateEmailTemplateAsync(int id, EmailTemplateDTO dto);
        Task<bool> DeleteEmailTemplateAsync(int id);
    }
}
