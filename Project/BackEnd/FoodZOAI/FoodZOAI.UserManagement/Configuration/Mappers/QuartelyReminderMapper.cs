using FoodZOAI.UserManagement.Models;
using FoodZOAI.UserManagement.Configuration.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace FoodZOAI.UserManagement.Mappers
{
    public class QuarterlyReminderMapper : IQuarterlyReminderMapper
    {
        public List<QuarterlyReminder> ListMapToDomain(List<QuarterlyReminderDTO> dtoList)
        {
            return dtoList?.Select(MapToDomain).ToList() ?? new List<QuarterlyReminder>();
        }

        public List<QuarterlyReminder> ListMapToDTO(List<QuarterlyReminder> domains)
        {
            throw new NotImplementedException();
        }

        public QuarterlyReminderDTO Map(QuarterlyReminder source)
        {
            if (source == null)
                return new QuarterlyReminderDTO();

            return new QuarterlyReminderDTO
            {
                Id = source.Id,
                ReminderId = source.ReminderId,
                Day = source.Day,
                Month = source.Month,
                Quarter = source.Quarter,
                CreatedAt = source.CreatedAt,
                UpdatedAt = source.UpdatedAt,
                DeletedAt = source.DeletedAt,
                IsActive = source.IsActive


            };
        }

        public List<QuarterlyReminderDTO> MapList(List<QuarterlyReminder> source)
        {
            return source?.Select(Map).ToList() ?? new List<QuarterlyReminderDTO>();
        }

        public QuarterlyReminder MapToDomain(QuarterlyReminderDTO dto)
        {
            if (dto == null)
                return new QuarterlyReminder();


            return new QuarterlyReminder
            {
                Id = dto.Id,
                ReminderId = dto.ReminderId,
                Day = dto.Day,
                Month = dto.Month,
                Quarter = dto.Quarter,
                CreatedAt = dto.CreatedAt,
                UpdatedAt = dto.UpdatedAt,
                DeletedAt = dto.DeletedAt,
                IsActive = dto.IsActive

            };
        }

        public QuarterlyReminderDTO MapToDTO(QuarterlyReminder domain)
        {
            return new QuarterlyReminderDTO
            {
                Id = domain.Id,
                ReminderId = domain.ReminderId,
                Day = domain.Day,
                Month = domain.Month,
                Quarter = domain.Quarter,
                CreatedAt = domain.CreatedAt,
                UpdatedAt = domain.UpdatedAt,
                DeletedAt = domain.DeletedAt,
                IsActive = domain.IsActive

            };
        }
    }
}
