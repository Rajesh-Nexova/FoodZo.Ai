using FoodZOAI.UserManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodZOAI.UserManagement.Repositories.Contracts
{
    public interface IWeeklyReminderRepository
    {
        Task<List<WeeklyReminder>> GetAllAsync();
        Task<WeeklyReminder?> GetByIdAsync(int id);
        Task<WeeklyReminder> AddAsync(WeeklyReminder entity);
        Task<WeeklyReminder> UpdateAsync(WeeklyReminder entity);
        Task<bool> DeleteAsync(int id);
    }
}
