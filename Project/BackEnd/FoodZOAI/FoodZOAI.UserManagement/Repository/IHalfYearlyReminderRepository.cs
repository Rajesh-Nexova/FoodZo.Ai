using FoodZOAI.UserManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodZOAI.UserManagement.Repositories
{
    public interface IHalfYearlyReminderRepository
    {
        Task<HalfYearlyReminder> GetByIdAsync(int id);
        Task<IEnumerable<HalfYearlyReminder>> GetAllAsync();
        Task AddAsync(HalfYearlyReminder entity);
        Task UpdateAsync(HalfYearlyReminder entity);
        Task DeleteAsync(int id);
    }
}
