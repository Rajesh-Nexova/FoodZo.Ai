using FoodZOAI.UserManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodZOAI.UserManagement.Repository
{
    public interface IYearlyReminderRepository
    {
        Task<List<YearlyReminder>> GetAllAsync();
        Task<YearlyReminder?> GetByIdAsync(int id);
        Task<YearlyReminder> CreateAsync(YearlyReminder reminder);
        Task<YearlyReminder> UpdateAsync(YearlyReminder reminder);
        Task<bool> DeleteAsync(int id);
    }
}
