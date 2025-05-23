using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement.Contracts
{
    public interface IOneTimeReminderMapper
    {
        OneTimeReminderDTO MapToDTO(OneTimeReminder reminder);
    }
}
