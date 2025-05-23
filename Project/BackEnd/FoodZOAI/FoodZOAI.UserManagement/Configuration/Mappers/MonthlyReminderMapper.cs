using FoodZOAI.UserManagement.Models;
using FoodZOAI.UserManagement.DTOs;

public class MonthlyReminderMapper : IMonthlyReminderMapper
{
    public MonthlyReminderDTO MapToDTO(MonthlyReminder reminder)
    {
        return new MonthlyReminderDTO
        {
            Id = reminder.Id,
            //Title = reminder.Title,
            //Description = reminder.Description,
            //ReminderDate = reminder.ReminderDate,
            //UserId = reminder.UserId,
            CreatedAt = reminder.CreatedAt,
            UpdatedAt = reminder.UpdatedAt,
            IsActive = reminder.IsActive
        };
    }

    public MonthlyReminder MapToDomain(MonthlyReminderDTO dto)
    {
        return new MonthlyReminder
        {
            Id = dto.Id,
            //Title = dto.Title,
            //Description = dto.Description,
            //ReminderDate = dto.ReminderDate,
            //UserId = dto.UserId,
            CreatedAt = dto.CreatedAt,
            UpdatedAt = dto.UpdatedAt,
            IsActive = dto.IsActive
        };
    }

    public IEnumerable<MonthlyReminderDTO> ListMapToDTO(IEnumerable<MonthlyReminder> reminders)
    {
        return reminders.Select(MapToDTO);
    }
}
