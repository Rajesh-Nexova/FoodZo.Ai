using FoodZOAI.UserManagement.Models;
using System.Collections.Generic;

namespace FoodZOAI.UserManagement.Configuration.Contracts
{
    public interface IUserNotificationMapper : IMapperService<UserNotification, UserNotificationDTO>
    {
        UserNotification MapToDomain(UserNotificationDTO dto);
        List<UserNotification> ListMapToDomain(List<UserNotificationDTO> dtoList);
    }
}
