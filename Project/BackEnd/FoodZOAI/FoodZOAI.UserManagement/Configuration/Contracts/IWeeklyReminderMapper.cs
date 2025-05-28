
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;
using System.Collections.Generic;

namespace FoodZOAI.UserManagement.Configuration.Contracts
{
    public interface IWeeklyReminderMapper
    {
        WeeklyReminder MapToDomain(WeeklyReminderDTO dto);
        List<WeeklyReminder> ListMapToDomain(List<WeeklyReminderDTO> dtoList);
    }
}
