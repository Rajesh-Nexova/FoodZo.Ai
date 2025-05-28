using System.Collections.Generic;
using FoodZOAI.UserManagement.Models;
using FoodZOAI.UserManagement.DTOs;

namespace FoodZOAI.UserManagement.Configuration.Contracts
{
    public interface IDailyReminderMapper : IMapperServices<DailyReminder, DailyReminderDTO>
    {
        DailyReminder MapToDomain(DailyReminderDTO dto);
        List<DailyReminder> ListMapToDomain(List<DailyReminderDTO> dtoList);
    }
}
