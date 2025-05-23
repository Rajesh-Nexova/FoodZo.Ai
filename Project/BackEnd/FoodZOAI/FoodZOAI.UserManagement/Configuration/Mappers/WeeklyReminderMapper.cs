using FoodZOAI.UserManagement.Configuration.Contracts;
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement.Mappers
{
    public class WeeklyReminderMapper : IWeeklyReminderMapper
    {
        public WeeklyReminderDTO MapToDTO(WeeklyReminder reminder)
        {
            return new WeeklyReminderDTO
            {
                Id = reminder.Id,
                Subject = reminder.Subject,
                Message = reminder.Message,
                Frequency = reminder.Frequency,
                StartDate = reminder.StartDate,
                EndDate = reminder.EndDate,
                DayOfWeek = reminder.DayOfWeek,
                IsRepeated = reminder.IsRepeated,
                IsEmailNotification = reminder.IsEmailNotification,
                IsActive = reminder.IsActive
            };
        }

        public WeeklyReminderDTO MapToDTO(WeeklyReminderDTO reminder)
        {
            throw new NotImplementedException();
        }

        public object MapToDTO(Reminder r)
        {
            throw new NotImplementedException();
        }

        public WeeklyReminder MapToEntity(WeeklyReminderDTO dto)
        {
            throw new NotImplementedException();
        }

        object IWeeklyReminderMapper.MapToDTO(WeeklyReminder reminder)
        {
            return MapToDTO(reminder);
        }
    }
}
