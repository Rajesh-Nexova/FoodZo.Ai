using FoodZOAI.UserManagement.Models;
using FoodZOAI.UserManagement.DTOs;
using System.Collections.Generic;

namespace FoodZOAI.UserManagement.Configuration.Contracts
{
    public interface IReminderService : IMapperService<Reminder, ReminderDTO>
    {

        Reminder MapToDomain(ReminderDTO dto);
        List<Reminder> ListMapToDomain(List<ReminderDTO> dtoList);
        ReminderDTO MapToDTO(Reminder domain);
        List<ReminderDTO> ListMapToDTO(List<Reminder> domains);
    }
    

}

