using FoodZOAI.UserManagement.DTOs;

public interface IYearlyReminderService
{
    Task<List<WeeklyReminderDTO>> GetYearlyRemindersAsync();
    Task<WeeklyReminderDTO?> GetYearlyReminderByIdAsync(int id);
}
