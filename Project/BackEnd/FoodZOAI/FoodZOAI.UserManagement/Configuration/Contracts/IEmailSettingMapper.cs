using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;


namespace FoodZOAI.UserManagement.Configuration.Contracts
{
    public interface IEmailSettingMapper : IMapperServices<EmailSetting, EmailSettingDTO>
    {
        EmailSetting MapToDomain(EmailSettingDTO dto);
        List<EmailSetting> ListMapToDomain(List<EmailSettingDTO> dtoList);
    }
}
