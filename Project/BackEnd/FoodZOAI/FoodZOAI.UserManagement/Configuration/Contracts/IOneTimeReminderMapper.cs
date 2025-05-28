using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement.Mappers.Interfaces
{
    public interface IOneTimeReminderMapper
    {
        OneTimeReminder MapToDomain(OneTimeReminderDTO dto);
        List<OneTimeReminder> ListMapToDomain(List<OneTimeReminderDTO> dtoList);
    }
}