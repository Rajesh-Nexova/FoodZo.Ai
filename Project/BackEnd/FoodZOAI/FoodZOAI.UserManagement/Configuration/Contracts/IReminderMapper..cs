using FoodZOAI.UserManagement.Models;
using System.Collections.Generic;

namespace FoodZOAI.UserManagement.Configuration.Contracts
{
    public interface IReminderMapper : IMapperService<Reminder, ReminderDTO>
    {
        Reminder MapToDomain(ReminderDTO dto);
        List<Reminder> ListMapToDomain(List<ReminderDTO> dtoList);
    }
}
