// IMonthlyReminderRepository.cs
using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement.Repositories.Contracts
{
    public interface IMonthlyReminderRepository
    {
        Task<List<MonthlyReminder>> GetAllAsync();
        Task<MonthlyReminder?> GetByIdAsync(int id);
        Task<MonthlyReminder> AddAsync(MonthlyReminder reminder);
        Task<MonthlyReminder> UpdateAsync(MonthlyReminder reminder);
        Task<bool> DeleteAsync(int id);
    }
}
