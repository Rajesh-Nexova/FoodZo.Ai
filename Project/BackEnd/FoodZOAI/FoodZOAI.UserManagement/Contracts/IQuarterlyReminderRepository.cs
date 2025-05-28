using FoodZOAI.UserManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodZOAI.UserManagement.Repositories.Contracts
{
    public interface IQuarterlyReminderRepository
    {
        Task<List<QuarterlyReminder>> GetAllAsync();
        Task<QuarterlyReminder?> GetByIdAsync(int id);
        Task<QuarterlyReminder> AddAsync(QuarterlyReminder entity);
        Task<QuarterlyReminder> UpdateAsync(QuarterlyReminder entity);
        Task<bool> DeleteAsync(int id);
    }
}
