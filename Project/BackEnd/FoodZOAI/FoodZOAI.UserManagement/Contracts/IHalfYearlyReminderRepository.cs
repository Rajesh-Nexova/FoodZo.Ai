using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement.Repositories.Contracts
{
    public interface IHalfYearlyReminderRepository
    {
        Task<List<HalfYearlyReminder>> GetAllAsync();
        Task<HalfYearlyReminder?> GetByIdAsync(int id);
        Task<HalfYearlyReminder> AddAsync(HalfYearlyReminder entity);
        Task<HalfYearlyReminder> UpdateAsync(HalfYearlyReminder entity);
        Task<bool> DeleteAsync(int id);
    }
}
