using FoodZOAI.UserManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodZOAI.UserManagement.Repositories
{
    public interface IMonthlyReminderRepository
    {
        Task<IEnumerable<MonthlyReminder>> GetAllAsync();
        Task<MonthlyReminder?> GetByIdAsync(int id);
    }
}
