using FoodZOAI.UserManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodZOAI.UserManagement.Repositories.Contracts
{
    public interface IDailyReminderRepository
    {
        Task<List<DailyReminder>> GetAllAsync();
        Task<DailyReminder?> GetByIdAsync(int id);
        Task<DailyReminder> AddAsync(DailyReminder reminder);
        Task<DailyReminder> UpdateAsync(DailyReminder reminder);
        Task<bool> DeleteAsync(int id);
    }
}
