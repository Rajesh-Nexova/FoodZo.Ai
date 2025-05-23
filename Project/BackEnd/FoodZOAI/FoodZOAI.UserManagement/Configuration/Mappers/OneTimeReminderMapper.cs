using FoodZOAI.UserManagement.Contracts;
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement.Configuration.Mappers
{
    public class OneTimeReminderMapper : IOneTimeReminderMapper
    {
        public OneTimeReminderDTO MapToDTO(OneTimeReminder reminder)
        {
            return new OneTimeReminderDTO
            {
                Id = reminder.Id,
                Message = reminder.Message,
                ReminderDate = reminder.ReminderDate,
                IsActive = reminder.IsActive
            };
        }
    }
}
