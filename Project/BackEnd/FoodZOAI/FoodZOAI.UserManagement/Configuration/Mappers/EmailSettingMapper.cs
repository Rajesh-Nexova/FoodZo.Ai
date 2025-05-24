using FoodZOAI.UserManagement.Configuration.Contracts;
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement.Configuration.Mappers
{
    public class EmailSettingMapper : IEmailSettingMapper
    {
        public List<EmailSetting> ListMapToDomain(List<EmailSettingDTO> dtoList)
        {
            return dtoList?.Select(MapToDomain).ToList() ?? new List<EmailSetting>();
        }

        public EmailSettingDTO Map(EmailSetting source)
        {
            if (source == null)
                return new EmailSettingDTO();

            return new EmailSettingDTO
            {
                Id = source.Id,
                Host = source.Host,
                UserName = source.UserName,
                Password = source.Password,
                IsEnableSsl = source.IsEnableSsl,
                IsDefault = source.IsDefault,
                IsActive = source.IsActive,
                CreatedByUser = source.CreatedByUser,
                ModifiedByUser = source.ModifiedByUser,
                DeletedByUser = source.DeletedByUser
            };
        }

        public List<EmailSettingDTO> MapList(List<EmailSetting> source)
        {
            return source?.Select(Map).ToList() ?? new List<EmailSettingDTO>();
        }

        public EmailSetting MapToDomain(EmailSettingDTO dto)
        {
            if (dto == null)
                return new EmailSetting();

            return new EmailSetting
            {

                Host = dto.Host,
                UserName = dto.UserName,
                Password = dto.Password,
                IsEnableSsl = dto.IsEnableSsl,
                IsDefault = dto.IsDefault,
                IsActive = dto.IsActive,
                CreatedByUser = dto.CreatedByUser,
                ModifiedByUser = dto.ModifiedByUser,
                DeletedByUser = dto.DeletedByUser
            };
        }
    }
}
