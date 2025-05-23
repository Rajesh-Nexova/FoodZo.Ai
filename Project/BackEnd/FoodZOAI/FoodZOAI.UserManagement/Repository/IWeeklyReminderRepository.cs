using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement.Repositories
{
    public interface IWeeklyReminderRepository
    {
        Task<IEnumerable<WeeklyReminder>> GetAllAsync();
        Task<WeeklyReminder?> GetByIdAsync(int id);
    }
}
