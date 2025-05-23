// File: Services/Interfaces/IMonthlyReminderService.cs
using FoodZOAI.UserManagement.DTOs;

public interface IMonthlyReminderService
{
    Task<IEnumerable<MonthlyReminderDTO>> GetMonthlyRemindersAsync();
    Task<MonthlyReminderDTO?> GetMonthlyReminderByIdAsync(int id); // ← Add this line
}
