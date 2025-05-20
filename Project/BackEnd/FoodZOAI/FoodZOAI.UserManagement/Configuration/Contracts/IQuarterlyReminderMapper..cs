using FoodZOAI.UserManagement.Models;
using System.Collections.Generic;

namespace FoodZOAI.UserManagement.Configuration.Contracts
{
    public interface IQuarterlyReminderMapper : IMapperService<QuarterlyReminder, QuarterlyReminderDTO>
    {
        QuarterlyReminder MapToDomain(QuarterlyReminderDTO dto);
        List<QuarterlyReminder> ListMapToDomain(List<QuarterlyReminderDTO> dtoList);
    }
}
