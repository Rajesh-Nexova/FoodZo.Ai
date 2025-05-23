using FoodZOAI.UserManagement.Models;
using FoodZOAI.UserManagement.Configuration.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace FoodZOAI.UserManagement.Mappers
{
    public class ReminderNotificationMapper : IReminderNotificationMapper
    {
        public ReminderNotificationDTO MapToDTO(ReminderNotification entity)
        {
            if (entity == null) return null;

            return new ReminderNotificationDTO
            {
                Id = entity.Id,
                ReminderId = entity.ReminderId,
                Subject = entity.Subject,
                Description = entity.Description,
                FetchDateTime = entity.FetchDateTime,
                IsDeleted = entity.IsDeleted,
                IsEmailNotification = entity.IsEmailNotification
            };
        }

        public ReminderNotification MapToDomain(ReminderNotificationDTO dto)
        {
            if (dto == null) return null;

            return new ReminderNotification
            {
                Id = dto.Id,
                ReminderId = dto.ReminderId,
                Subject = dto.Subject,
                Description = dto.Description,
                FetchDateTime = dto.FetchDateTime,
                IsDeleted = dto.IsDeleted,
                IsEmailNotification = dto.IsEmailNotification
            };
        }

        public List<ReminderNotificationDTO> ListMapToDTO(List<ReminderNotification> entityList)
        {
            return entityList?.Select(MapToDTO).ToList() ?? new List<ReminderNotificationDTO>();
        }

        public List<ReminderNotification> ListMapToDomain(List<ReminderNotificationDTO> dtoList)
        {
            return dtoList?.Select(MapToDomain).ToList() ?? new List<ReminderNotification>();
        }

        public ReminderNotificationDTO Map(ReminderNotification source)
        {
            throw new NotImplementedException();
        }

        public List<ReminderNotificationDTO> MapList(List<ReminderNotification> source)
        {
            throw new NotImplementedException();
        }
    }
}
