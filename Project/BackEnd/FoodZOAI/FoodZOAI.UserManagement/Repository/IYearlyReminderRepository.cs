using FoodZOAI.UserManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodZOAI.UserManagement.Repositories
{
    public interface IYearlyReminderRepository
    {
        Task<IEnumerable<YearlyReminder>> GetAllAsync();
        Task<YearlyReminder?> GetByIdAsync(int id);
    }
}
