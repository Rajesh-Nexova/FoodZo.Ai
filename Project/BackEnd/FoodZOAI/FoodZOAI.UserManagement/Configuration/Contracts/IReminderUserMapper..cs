using FoodZOAI.UserManagement.Models;
using System.Collections.Generic;

namespace FoodZOAI.UserManagement.Configuration.Contracts
{
    public interface IReminderUserMapper : IMapperServices<ReminderUser, ReminderUserDTO>
    {
        ReminderUser MapToDomain(ReminderUserDTO dto);
        List<ReminderUser> ListMapToDomain(List<ReminderUserDTO> dtoList);
    }
}
