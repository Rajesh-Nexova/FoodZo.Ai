using FoodZOAI.UserManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodZOAI.UserManagement.Repositories
{
    public interface IReminderRepository
    {
        Task<Reminder> GetByIdAsync(int id);
        Task<IEnumerable<Reminder>> GetAllAsync();
        Task AddAsync(Reminder entity);
        Task UpdateAsync(Reminder entity);
        Task DeleteAsync(int id);
    }
}
