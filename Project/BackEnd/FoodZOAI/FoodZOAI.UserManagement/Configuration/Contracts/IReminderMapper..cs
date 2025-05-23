using FoodZOAI.UserManagement.Models;
using System.Collections.Generic;

namespace FoodZOAI.UserManagement.Configuration.Contracts
{
    public interface IReminderMapper : IMapperServices<Reminder, ReminderDTO>
    {
        Reminder MapToDomain(ReminderDTO dto);
        List<Reminder> ListMapToDomain(List<ReminderDTO> dtoList);
        ReminderDTO MapToDTO(Reminder r);
        
        List<ReminderDTO> ListMapToDTO(List<Reminder> domains);
    }
}
