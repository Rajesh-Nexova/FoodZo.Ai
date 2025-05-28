using FoodZOAI.UserManagement.Configuration.Contracts;
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement.Configuration.Mappers
{
    public class HalfYearlyReminderMapper : IHalfYearlyReminderMapper
    {
        public HalfYearlyReminder MapToDomain(HalfYearlyReminderDTO dto)
        {
            return new HalfYearlyReminder
            {
                Id = dto.Id,
                ReminderId = dto.ReminderId,
                Day = dto.Day,
                Month = dto.Month,
                Quarter = dto.Quarter,
                IsActive = dto.IsActive
            };
        }

        public List<HalfYearlyReminder> ListMapToDomain(List<HalfYearlyReminderDTO> dtoList)
        {
            return dtoList.Select(MapToDomain).ToList();
        }
    }
}
