using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement.Repositories.Interfaces
{
    public interface IOneTimeReminderRepository
    {
        Task<List<OneTimeReminder>> GetAllAsync();
        Task<OneTimeReminder?> GetByIdAsync(int id);
        Task AddAsync(OneTimeReminder reminder);
        Task UpdateAsync(OneTimeReminder reminder);
        Task DeleteAsync(int id);
    }
}