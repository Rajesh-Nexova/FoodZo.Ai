using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement.Contracts
{
    
        public interface IQuarterlyReminderRepository
        {
            Task<IEnumerable<QuarterlyReminder>> GetAllAsync();
            Task<QuarterlyReminder> GetByIdAsync(int id);
            Task AddAsync(QuarterlyReminder entity);
            Task UpdateAsync(QuarterlyReminder entity);
            Task DeleteAsync(int id);
        }
    }

