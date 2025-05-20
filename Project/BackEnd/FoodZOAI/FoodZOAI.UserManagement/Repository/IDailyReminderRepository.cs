using FoodZOAI.UserManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodZOAI.UserManagement.Repositories
{
    public interface IDailyReminderRepository
    {
        Task<DailyReminder> GetByIdAsync(int id);
        Task<IEnumerable<DailyReminder>> GetAllAsync();
        Task AddAsync(DailyReminder entity);
        Task UpdateAsync(DailyReminder entity);
        Task DeleteAsync(int id);
    }
}
