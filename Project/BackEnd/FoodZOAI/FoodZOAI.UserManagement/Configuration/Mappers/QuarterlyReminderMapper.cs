using FoodZOAI.UserManagement.Models;
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Configuration.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace FoodZOAI.UserManagement.Configuration.Mappers
{
    public class QuarterlyReminderMapper : IQuarterlyReminderMapper
    {
        public QuarterlyReminder MapToDomain(QuarterlyReminderDTO dto)
        {
            return new QuarterlyReminder
            {
                Id = dto.Id,
                ReminderId = dto.ReminderId,
                Day = dto.Day,
                Month = dto.Month,
                Quarter = dto.Quarter,
                IsActive = dto.IsActive
            };
        }

        public List<QuarterlyReminder> ListMapToDomain(List<QuarterlyReminderDTO> dtoList)
        {
            return dtoList.Select(MapToDomain).ToList();
        }
    }
}
