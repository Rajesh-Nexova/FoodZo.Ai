using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement.Configuration.Contracts
{
    public interface IWeeklyReminderMapper
    {
        WeeklyReminderDTO MapToDTO(WeeklyReminderDTO reminder);
        object MapToDTO(Reminder r);
        object MapToDTO(WeeklyReminder reminder);
        WeeklyReminder MapToEntity(WeeklyReminderDTO dto); 
    }

}
