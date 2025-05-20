using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement.Contracts
{
    public interface IEmailTemplateRepository
    {
        Task<IEnumerable<EmailTemplate>> GetAllAsync();
        Task<EmailTemplate?> GetByIdAsync(int id);
        Task AddAsync(EmailTemplate emailTemplate);
        Task UpdateAsync(EmailTemplate emailTemplate);
        Task DeleteAsync(int id);

        Task<bool> ExistsAsync(int id);
    }
}
