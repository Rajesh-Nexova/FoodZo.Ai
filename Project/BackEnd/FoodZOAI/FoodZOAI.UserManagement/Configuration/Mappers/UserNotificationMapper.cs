using FoodZOAI.UserManagement.Models;
using FoodZOAI.UserManagement.Configuration.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace FoodZOAI.UserManagement.Mappers
{
    public class UserNotificationMapper : IUserNotificationMapper
    {
        public UserNotificationDTO MapToDTO(UserNotification entity)
        {
            if (entity == null) return null;

            return new UserNotificationDTO
            {
                Id = entity.Id,
                UserId = entity.UserId,
                Message = entity.Message,
                IsRead = entity.IsRead,
                NotificationsType = entity.NotificationsType
            };
        }

        public UserNotification MapToDomain(UserNotificationDTO dto)
        {
            if (dto == null) return null;

            return new UserNotification
            {
                Id = dto.Id,
                UserId = dto.UserId,
                Message = dto.Message,
                IsRead = dto.IsRead,
                NotificationsType = dto.NotificationsType
            };
        }

        public List<UserNotificationDTO> ListMapToDTO(List<UserNotification> entityList)
        {
            return entityList?.Select(MapToDTO).ToList() ?? new List<UserNotificationDTO>();
        }

        public List<UserNotification> ListMapToDomain(List<UserNotificationDTO> dtoList)
        {
            return dtoList?.Select(MapToDomain).ToList() ?? new List<UserNotification>();
        }

        public UserNotificationDTO Map(UserNotification source)
        {
            throw new NotImplementedException();
        }

        public List<UserNotificationDTO> MapList(List<UserNotification> source)
        {
            throw new NotImplementedException();
        }
    }
}
