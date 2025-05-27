using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement.Contracts
{
    public interface IEmailTemplateRepository
    {
        Task<EmailTemplateDTO?> GetTemplateByIdAsync(int id);
        Task<IEnumerable<EmailTemplate>> GetAllAsync();
        Task<EmailTemplate?> GetByIdAsync(int id);
        Task AddAsync(EmailTemplate emailTemplate);
        Task UpdateAsync(EmailTemplate emailTemplate);
        Task<bool> DeleteAsync(int id);

        Task<bool> ExistsAsync(int id);
    }
}
