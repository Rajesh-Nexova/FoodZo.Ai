using FoodZOAI.UserManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodZOAI.UserManagement.Repositories
{
    public interface IReminderSchedulerRepository
    {
        Task<ReminderScheduler> GetByIdAsync(int id);
        Task<IEnumerable<ReminderScheduler>> GetAllAsync();
        Task AddAsync(ReminderScheduler entity);
        Task UpdateAsync(ReminderScheduler entity);
        Task DeleteAsync(int id);
    }
}
