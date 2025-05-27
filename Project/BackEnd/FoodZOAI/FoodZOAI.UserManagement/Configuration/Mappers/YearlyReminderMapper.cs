using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;
using System.Collections.Generic;
using System.Linq;

namespace FoodZOAI.UserManagement.Configuration.Mappers
{
    public class YearlyReminderMapper : IYearlyReminderMapper
    {
        public YearlyReminder MapToDomain(YearlyReminderDTO dto)
        {
            return new YearlyReminder
            {
                Id = dto.Id,
                ReminderId = dto.ReminderId,
                Day = dto.Day,
                Month = dto.Month,
                IsActive = dto.IsActive
            };
        }

        public List<YearlyReminder> ListMapToDomain(List<YearlyReminderDTO> dtoList)
        {
            return dtoList.Select(MapToDomain).ToList();
        }
    }
}
