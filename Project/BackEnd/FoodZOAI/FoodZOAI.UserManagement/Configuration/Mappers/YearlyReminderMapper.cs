using FoodZOAI.UserManagement.Models;
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Mappers.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace FoodZOAI.UserManagement.Mappers.Implementations
{
    public class YearlyReminderMapper : IYearlyReminderMapper
    {
        public YearlyReminder MapToDomain(YearlyReminderDTO dto)
        {
            if (dto == null) return null;

            return new YearlyReminder
            {
                Id = dto.Id,
                ReminderId = dto.ReminderId,
                Day = dto.Day,
                Month = dto.Month,
                IsActive = dto.IsActive,
                CreatedAt = dto.CreatedAt,
                UpdatedAt = dto.UpdatedAt,
                DeletedAt = dto.DeletedAt
            };
        }

        public YearlyReminderDTO MapToDTO(YearlyReminder domain)
        {
            if (domain == null) return null;

            return new YearlyReminderDTO
            {
                Id = domain.Id,
                ReminderId = domain.ReminderId,
                Day = domain.Day,
                Month = domain.Month,
                IsActive = domain.IsActive,
                CreatedAt = domain.CreatedAt ?? DateTime.MinValue,
                UpdatedAt = domain.UpdatedAt,
                DeletedAt = domain.DeletedAt
            };
        }

        public List<YearlyReminderDTO> ListMapToDTO(List<YearlyReminder> domains)
        {
            if (domains == null) return new List<YearlyReminderDTO>();
            return domains.Select(MapToDTO).ToList();
        }
    }
}
