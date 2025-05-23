using FoodZOAI.UserManagement.Models;
using System.Collections.Generic;

namespace FoodZOAI.UserManagement.Configuration.Contracts
{
    public interface IQuarterlyReminderMapper : IMapperServices<QuarterlyReminder, QuarterlyReminderDTO>
    {
        QuarterlyReminder MapToDomain(QuarterlyReminderDTO dto);
        List<QuarterlyReminder> ListMapToDomain(List<QuarterlyReminderDTO> dtoList);
        QuarterlyReminderDTO MapToDTO(QuarterlyReminder domain);
        List<QuarterlyReminder> ListMapToDTO(List<QuarterlyReminder> domains);
    }
}
