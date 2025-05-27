using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement.Configuration.Contracts
{
    public interface IHalfYearlyReminderMapper
    {
        HalfYearlyReminder MapToDomain(HalfYearlyReminderDTO dto);
        List<HalfYearlyReminder> ListMapToDomain(List<HalfYearlyReminderDTO> dtoList);
    }
}
