using FoodZOAI.UserManagement.Models;
using FoodZOAI.UserManagement.Configuration.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace FoodZOAI.UserManagement.Mappers
{
    public class ReminderSchedulerMapper : IReminderSchedulerMapper
    {
        public ReminderSchedulerDTO MapToDTO(ReminderScheduler entity)
        {
            if (entity == null) return null;

            return new ReminderSchedulerDTO
            {
                Id = entity.Id,
                Duration = entity.Duration,
                IsActive = entity.IsActive,
                Frequency = entity.Frequency,
                CreatedDate = entity.CreatedDate,
                UserId = entity.UserId,
                ReminderId = entity.ReminderId,
                IsRead = entity.IsRead,
                IsEmailNotification = entity.IsEmailNotification,
                Subject = entity.Subject,
                Message = entity.Message
            };
        }

        public ReminderScheduler MapToDomain(ReminderSchedulerDTO dto)
        {
            if (dto == null) return null;

            return new ReminderScheduler
            {
                Id = dto.Id,
                Duration = dto.Duration,
                IsActive = dto.IsActive,
                Frequency = dto.Frequency,
                CreatedDate = dto.CreatedDate,
                UserId = dto.UserId,
                ReminderId = dto.ReminderId,
                IsRead = dto.IsRead,
                IsEmailNotification = dto.IsEmailNotification,
                Subject = dto.Subject,
                Message = dto.Message
            };
        }

        public List<ReminderSchedulerDTO> ListMapToDTO(List<ReminderScheduler> entityList)
        {
            return entityList?.Select(MapToDTO).ToList() ?? new List<ReminderSchedulerDTO>();
        }

        public List<ReminderScheduler> ListMapToDomain(List<ReminderSchedulerDTO> dtoList)
        {
            return dtoList?.Select(MapToDomain).ToList() ?? new List<ReminderScheduler>();
        }

        public ReminderSchedulerDTO Map(ReminderScheduler source)
        {
            throw new NotImplementedException();
        }

        public List<ReminderSchedulerDTO> MapList(List<ReminderScheduler> source)
        {
            throw new NotImplementedException();
        }
    }
}
