using FoodZOAI.UserManagement.Models;
using System.Collections.Generic;

namespace FoodZOAI.UserManagement.Configuration.Contracts
{
    public interface IReminderNotificationMapper : IMapperServices<ReminderNotification, ReminderNotificationDTO>
    {
        ReminderNotification MapToDomain(ReminderNotificationDTO dto);
        List<ReminderNotification> ListMapToDomain(List<ReminderNotificationDTO> dtoList);
    }
}
