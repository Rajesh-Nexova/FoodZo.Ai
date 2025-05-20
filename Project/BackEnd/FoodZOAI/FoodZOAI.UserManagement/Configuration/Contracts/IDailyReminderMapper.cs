using FoodZOAI.UserManagement.Models;
using System.Collections.Generic;

namespace FoodZOAI.UserManagement.Configuration.Contracts
{
    public interface IDailyReminderMapper : IMapperService<DailyReminder, DailyReminderDTO>
    {
        DailyReminder MapToDomain(DailyReminderDTO dto);
        List<DailyReminder> ListMapToDomain(List<DailyReminderDTO> dtoList);
    }
}
