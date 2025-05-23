using FoodZOAI.UserManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodZOAI.UserManagement.Repositories
{
    public interface IQuarterlyReminderRepository
    {
        Task<IEnumerable<QuarterlyReminder>> GetAllAsync();
        Task<QuarterlyReminder?> GetByIdAsync(int id);
    }
}
