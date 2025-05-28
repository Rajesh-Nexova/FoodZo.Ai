using FoodZOAI.UserManagement.Configuration.Contracts;
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement.Configuration.Mappers
{
    public class MonthlyReminderMapper : IMonthlyReminderMapper
    {
        public MonthlyReminder MapToDomain(MonthlyReminderDTO dto)
        {
            return new MonthlyReminder
            {
                Id = dto.Id,
                Subject = dto.Subject,
                Message = dto.Message,
                Frequency = dto.Frequency,
                DayOfMonth = dto.DayOfMonth,
                StartDate = dto.StartDate,
                IsRepeated = dto.IsRepeated,
                IsEmailNotification = dto.IsEmailNotification,
                IsActive = dto.IsActive,
                CreatedByUser = dto.CreatedByUser
            };
        }

        public List<MonthlyReminder> ListMapToDomain(List<MonthlyReminderDTO> dtoList)
        {
            return dtoList.Select(MapToDomain).ToList();
        }
    }
}
