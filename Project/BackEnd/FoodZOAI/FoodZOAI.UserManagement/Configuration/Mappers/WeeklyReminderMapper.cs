
using FoodZOAI.UserManagement.Configuration.Contracts;
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;
using System.Collections.Generic;
using System.Linq;

namespace FoodZOAI.UserManagement.Configuration
{
    public class WeeklyReminderMapper : IWeeklyReminderMapper
    {
        public WeeklyReminder MapToDomain(WeeklyReminderDTO dto)
        {
            if (dto == null) return null!;

            return new WeeklyReminder
            {
                Id = dto.Id,
                Subject = dto.Subject,
                Message = dto.Message,
                Frequency = dto.Frequency,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                DayOfWeek = dto.DayOfWeek,
                IsRepeated = dto.IsRepeated,
                IsEmailNotification = dto.IsEmailNotification,
                IsActive = dto.IsActive

                // Do NOT assign CreatedAt, ModifiedAt, etc. here
                // because they are NOT part of WeeklyReminderDTO
            };
        }

        public List<WeeklyReminder> ListMapToDomain(List<WeeklyReminderDTO> dtoList)
        {
            if (dtoList == null) return new List<WeeklyReminder>();

            return dtoList.Select(MapToDomain).ToList();
        }
    }
}
