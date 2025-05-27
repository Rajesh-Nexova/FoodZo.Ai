using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;
using FoodZOAI.UserManagement.Configuration.Contracts;
using System.Collections.Generic;

namespace FoodZOAI.UserManagement.Configuration
{
    public class DailyReminderMapper : IDailyReminderMapper
    {
        public DailyReminder MapToDomain(DailyReminderDTO dto)
        {
            return new DailyReminder
            {
                Id = dto.Id,
                ReminderId = dto.ReminderId,
                DayOfWeek = dto.DayOfWeek,
                CreatedAt = dto.CreatedAt,
                UpdatedAt = dto.UpdatedAt,
                DeletedAt = dto.DeletedAt,
                IsActive = dto.IsActive
                // Reminder navigation property is not mapped here, assuming DTO doesn't contain it
            };
        }

        public List<DailyReminder> ListMapToDomain(List<DailyReminderDTO> dtoList)
        {
            var domainList = new List<DailyReminder>();
            foreach (var dto in dtoList)
            {
                domainList.Add(MapToDomain(dto));
            }
            return domainList;
        }

        public DailyReminderDTO Map(DailyReminder source)
        {
            throw new NotImplementedException();
        }

        public List<DailyReminderDTO> MapList(List<DailyReminder> source)
        {
            throw new NotImplementedException();
        }
    }
}
