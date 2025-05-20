using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement.Configuration.Contracts
{
    public interface IEmailTemplateMapper : IMapperService<EmailTemplate, EmailTemplateDTO>
    {
        EmailTemplate MapToDomain(EmailTemplateDTO dto);
        List<EmailTemplate> ListMapToDomain(List<EmailTemplateDTO> domainDtos);

        EmailTemplateDTO MapToDTO(EmailTemplate domain);
        List<EmailTemplateDTO> ListMapToDTO(List<EmailTemplate> domains);
    }
}
