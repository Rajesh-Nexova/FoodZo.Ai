using FoodZOAI.UserManagement.Models;
using FoodZOAI.UserManagement.Configuration.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace FoodZOAI.UserManagement.Mappers
{
    public class DailyReminderMapper : IDailyReminderMapper
    {
        public List<DailyReminder> ListMapToDomain(List<DailyReminderDTO> dtoList)
        {
            return dtoList?.Select(MapToDomain).ToList() ?? new List<DailyReminder>();
        }

        public List<DailyReminder> ListMapToDTO(List<DailyReminder> domains)
        {
            throw new NotImplementedException();
        }

        /*public List<DailyReminder> ListMapToDTO(List<DailyReminder> domains)
        {
            return domains.Select(MapToDTO).ToList();
        }*/

        public DailyReminderDTO Map(DailyReminder source)
        {
            if (source == null)
                return new DailyReminderDTO();

            return new DailyReminderDTO
            {
                Id = source.Id,
                ReminderId = source.ReminderId,
                DayOfWeek = source.DayOfWeek,
                CreatedAt = source.CreatedAt,
                UpdatedAt = source.UpdatedAt,
                DeletedAt = source.DeletedAt,
                IsActive = source.IsActive

            };
        }

        public List<DailyReminderDTO> MapList(List<DailyReminder> source)
        {
            return source?.Select(Map).ToList() ?? new List<DailyReminderDTO>();
        }

        public DailyReminder MapToDomain(DailyReminderDTO dto)
        {

            if (dto == null)
                return new DailyReminder();


            return new DailyReminder
            {

                Id = dto.Id,
                ReminderId = dto.ReminderId,
                DayOfWeek = dto.DayOfWeek,
                CreatedAt = dto.CreatedAt,
                UpdatedAt = dto.UpdatedAt,
                DeletedAt = dto.DeletedAt,
                IsActive = dto.IsActive
            };
        }

        public DailyReminderDTO MapToDTO(DailyReminder domain)
        {
            return new DailyReminderDTO
            {
                Id = domain.Id,
                ReminderId = domain.ReminderId,
                DayOfWeek = domain.DayOfWeek,
                CreatedAt = domain.CreatedAt,
                UpdatedAt = domain.UpdatedAt,
                DeletedAt = domain.DeletedAt,
                IsActive = domain.IsActive
            };
        }
    }
}
