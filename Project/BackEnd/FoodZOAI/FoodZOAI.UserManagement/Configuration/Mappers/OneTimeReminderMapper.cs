using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Mappers.Interfaces;
using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement.Mappers.Implementations
{
    public class OneTimeReminderMapper : IOneTimeReminderMapper
    {
        public OneTimeReminder MapToDomain(OneTimeReminderDTO dto)
        {
            return new OneTimeReminder
            {
                Id = dto.Id,
                Message = dto.Message,
                ReminderDate = dto.ReminderDate,
                IsActive = dto.IsActive,
                //CreatedAt = dto.CreatedAt,
                //DeletedAt = dto.DeletedAt
            };
        }

        public List<OneTimeReminder> ListMapToDomain(List<OneTimeReminderDTO> dtoList)
        {
            return dtoList.Select(MapToDomain).ToList();
        }
    }
}