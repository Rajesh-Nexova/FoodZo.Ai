using FoodZOAI.UserManagement.Models;
using FoodZOAI.UserManagement.Configuration.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace FoodZOAI.UserManagement.Mappers
{
    public class ReminderMapper : IReminderMapper
    {
        public List<Reminder> ListMapToDomain(List<ReminderDTO> dtoList)
        {
            return dtoList?.Select(MapToDomain).ToList() ?? new List<Reminder>();
        }

        public List<ReminderDTO> ListMapToDTO(List<Reminder> domains)
        {
            throw new NotImplementedException();
        }

        public ReminderDTO Map(Reminder source) =>
     source == null ? new ReminderDTO() : new ReminderDTO
     {
         Id = source.Id,
         Subject = source.Subject,
         Message = source.Message,
         Frequency = source.Frequency,
         StartDate = source.StartDate,
         EndDate = source.EndDate,
         DayOfWeek = source.DayOfWeek,
         IsRepeated = source.IsRepeated,
         IsEmailNotification = source.IsEmailNotification,
         CreatedAt = source.CreatedAt,
         ModifiedAt = source.ModifiedAt,
         DeletedAt = source.DeletedAt,
         IsActive = source.IsActive,
         CreatedByUser = source.CreatedByUser,
         ModifiedByUser = source.ModifiedByUser,
         DeletedByUser = source.DeletedByUser
     };

        public List<ReminderDTO> MapList(List<Reminder> source)
        {
            return source?.Select(Map).ToList() ?? new List<ReminderDTO>();
        }

        public Reminder MapToDomain(ReminderDTO dto)
        {
            if (dto == null)
                return new Reminder();


            return new Reminder
            {

                Id = dto.Id,
                Subject = dto.Subject,
                Message = dto.Message,
                Frequency = dto.Frequency,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                DayOfWeek = dto.DayOfWeek,
                IsRepeated = dto.IsRepeated,
                IsEmailNotification = dto.IsEmailNotification,
                CreatedAt = dto.CreatedAt,
                ModifiedAt = dto.ModifiedAt,
                DeletedAt = dto.DeletedAt,
                IsActive = dto.IsActive,
                CreatedByUser = dto.CreatedByUser,
                ModifiedByUser = dto.ModifiedByUser,
                DeletedByUser = dto.DeletedByUser
            };

        }

        public object MapToDTO(Reminder r)
        {
            throw new NotImplementedException();
        }

        ReminderDTO IReminderMapper.MapToDTO(Reminder r)
        {
            throw new NotImplementedException();
        }
    }
}
