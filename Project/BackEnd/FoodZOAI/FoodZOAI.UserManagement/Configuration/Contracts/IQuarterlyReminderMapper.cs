using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;
using System.Collections.Generic;

namespace FoodZOAI.UserManagement.Configuration.Contracts
{
    public interface IQuarterlyReminderMapper
    {
        QuarterlyReminder MapToDomain(QuarterlyReminderDTO dto);
        List<QuarterlyReminder> ListMapToDomain(List<QuarterlyReminderDTO> dtoList);
    }
}
