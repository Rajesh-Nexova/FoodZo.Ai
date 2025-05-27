using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;
using System.Collections.Generic;

namespace FoodZOAI.UserManagement.Configuration.Mappers
{
    public interface IYearlyReminderMapper
    {
        YearlyReminder MapToDomain(YearlyReminderDTO dto);
        List<YearlyReminder> ListMapToDomain(List<YearlyReminderDTO> dtoList);
    }
}
