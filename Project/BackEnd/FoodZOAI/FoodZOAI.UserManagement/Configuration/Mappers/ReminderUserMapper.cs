using FoodZOAI.UserManagement.Models;
using FoodZOAI.UserManagement.Configuration.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace FoodZOAI.UserManagement.Mappers
{
    public class ReminderUserMapper : IReminderUserMapper
    {
        public ReminderUserDTO MapToDTO(ReminderUser entity)
        {
            if (entity == null) return null;

            return new ReminderUserDTO
            {
                ReminderId = entity.ReminderId,
                UserId = entity.UserId
            };
        }

        public ReminderUser MapToDomain(ReminderUserDTO dto)
        {
            if (dto == null) return null;

            return new ReminderUser
            {
                ReminderId = dto.ReminderId,
                UserId = dto.UserId
            };
        }

        public List<ReminderUserDTO> ListMapToDTO(List<ReminderUser> entityList)
        {
            return entityList?.Select(MapToDTO).ToList() ?? new List<ReminderUserDTO>();
        }

        public List<ReminderUser> ListMapToDomain(List<ReminderUserDTO> dtoList)
        {
            return dtoList?.Select(MapToDomain).ToList() ?? new List<ReminderUser>();
        }

        public ReminderUserDTO Map(ReminderUser source)
        {
            throw new NotImplementedException();
        }

        public List<ReminderUserDTO> MapList(List<ReminderUser> source)
        {
            throw new NotImplementedException();
        }
    }
}
