using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement.Configuration.Contracts
{
    public interface IEmailTemplateMapper : IMapperServices<EmailTemplate, EmailTemplateDTO>
    {
        EmailTemplate MapToDomain(EmailTemplateDTO dto);
        List<EmailTemplate> ListMapToDomain(List<EmailTemplateDTO> domainDtos);

       
    }
}
