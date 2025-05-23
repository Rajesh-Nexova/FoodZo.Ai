using FoodZOAI.UserManagement.Models;
using System.Collections.Generic;

namespace FoodZOAI.UserManagement.Configuration.Contracts
{
    public interface IDailyReminderMapper : IMapperServices<DailyReminder, DailyReminderDTO>
    {
        DailyReminder MapToDomain(DailyReminderDTO dto);
        List<DailyReminder> ListMapToDomain(List<DailyReminderDTO> dtoList);
        DailyReminderDTO MapToDTO(DailyReminder domain);
        List<DailyReminder> ListMapToDTO(List<DailyReminder> domains);
    }
}
