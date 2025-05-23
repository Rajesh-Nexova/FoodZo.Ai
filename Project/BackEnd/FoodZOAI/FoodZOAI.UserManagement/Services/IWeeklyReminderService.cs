using FoodZOAI.UserManagement.DTOs;

namespace FoodZOAI.UserManagement.Services
{
    public interface IWeeklyReminderService
    {
        Task<List<WeeklyReminderDTO>> GetAllWeeklyRemindersAsync();
    }
}
