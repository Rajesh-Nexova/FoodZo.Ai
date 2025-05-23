using FoodZOAI.UserManagement.Models;
using FoodZOAI.UserManagement.DTOs;
using System.Collections.Generic;

namespace FoodZOAI.UserManagement.Mappers.Interfaces
{
    public interface IYearlyReminderMapper
    {
        YearlyReminder MapToDomain(YearlyReminderDTO dto);
        YearlyReminderDTO MapToDTO(YearlyReminder domain);
        List<YearlyReminderDTO> ListMapToDTO(List<YearlyReminder> domains);
    }
}
