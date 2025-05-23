using FoodZOAI.UserManagement.Configuration.Contracts;
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;
using System.Collections.Generic;

namespace FoodZOAI.UserManagement.Mappers.Interfaces
{
    public interface IHalfYearlyReminderMapper : IMapperServices<HalfYearlyReminder, HalfYearlyReminderDTO>
    {
        HalfYearlyReminder MapToDomain(HalfYearlyReminderDTO dto);
        List<HalfYearlyReminder> ListMapToDomain(List<HalfYearlyReminderDTO> dtoList);

        HalfYearlyReminderDTO MapToDTO(HalfYearlyReminder domain);
        List<HalfYearlyReminderDTO> ListMapToDTO(List<HalfYearlyReminder> domains);
    }
}
