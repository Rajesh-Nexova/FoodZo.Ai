using FoodZOAI.UserManagement.Configuration.Contracts;
using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement
{
    public interface IHalfYearlyReminderMapper : IMapperService<HalfYearlyReminder, HalfYearlyReminderDTO>
    {
        HalfYearlyReminder MapToDomain(HalfYearlyReminderDTO dto);
        List<HalfYearlyReminder> ListMapToDomain(List<HalfYearlyReminderDTO> dtoList);
    }
}
