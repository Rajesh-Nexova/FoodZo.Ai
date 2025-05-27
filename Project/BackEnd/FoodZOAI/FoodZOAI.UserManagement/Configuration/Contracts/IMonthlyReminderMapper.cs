using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement.Configuration.Contracts
{
    public interface IMonthlyReminderMapper
    {
        MonthlyReminder MapToDomain(MonthlyReminderDTO dto);
        List<MonthlyReminder> ListMapToDomain(List<MonthlyReminderDTO> dtoList);
    }
}
