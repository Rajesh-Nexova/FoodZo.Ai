using FoodZOAI.UserManagement.Models;
using FoodZOAI.UserManagement.DTOs;

public interface IMonthlyReminderMapper
{
    MonthlyReminderDTO MapToDTO(MonthlyReminder reminder);
    MonthlyReminder MapToDomain(MonthlyReminderDTO dto);

    IEnumerable<MonthlyReminderDTO> ListMapToDTO(IEnumerable<MonthlyReminder> reminders); // ← Add this
}
