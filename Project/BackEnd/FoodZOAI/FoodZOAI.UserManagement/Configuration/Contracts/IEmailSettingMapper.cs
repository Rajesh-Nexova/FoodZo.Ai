using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;


namespace FoodZOAI.UserManagement.Configuration.Contracts
{
    public interface IEmailSettingMapper : IMapperService<EmailSetting, EmailSettingDTO>
    {
        EmailSetting MapToDomain(EmailSettingDTO dto);
        List<EmailSetting> ListMapToDomain(List<EmailSettingDTO> domainDtos);

        EmailSettingDTO MapToDTO(EmailSetting domain);
        List<EmailSettingDTO> ListMapToDTO(List<EmailSetting> domains);
    }
}
