using FoodZOAI.UserManagement.Models;
using System.Collections.Generic;

namespace FoodZOAI.UserManagement.Configuration.Contracts
{
    public interface IReminderSchedulerMapper : IMapperServices<ReminderScheduler, ReminderSchedulerDTO>
    {
        ReminderScheduler MapToDomain(ReminderSchedulerDTO dto);
        List<ReminderScheduler> ListMapToDomain(List<ReminderSchedulerDTO> dtoList);
    }
}
