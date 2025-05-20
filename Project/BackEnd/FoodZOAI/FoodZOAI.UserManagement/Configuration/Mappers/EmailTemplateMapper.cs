using FoodZOAI.UserManagement.Configuration.Contracts;
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement.Configuration.Mappers
{
    public class EmailTemplateMapper : IEmailTemplateMapper
    {
        public EmailTemplate MapToDomain(EmailTemplateDTO dto)
        {
            return new EmailTemplate
            {
                
                Name = dto.Name,
                Subject = dto.Subject,
                Body = dto.Body,
                IsActive = dto.IsActive,
                CreatedByUser = dto.CreatedByUser,
                ModifiedByUser = dto.ModifiedByUser,
                DeletedByUser = dto.DeletedByUser
            };
        }

        public List<EmailTemplate> ListMapToDomain(List<EmailTemplateDTO> domainDtos)
        {
            return domainDtos.Select(MapToDomain).ToList();
        }

        public EmailTemplateDTO MapToDTO(EmailTemplate domain)
        {
            return new EmailTemplateDTO
            {
                Id = domain.Id,
                Name = domain.Name,
                Subject = domain.Subject,
                Body = domain.Body,
                IsActive = domain.IsActive,
                CreatedByUser = domain.CreatedByUser,
                ModifiedByUser = domain.ModifiedByUser,
                DeletedByUser = domain.DeletedByUser
            };
        }

        public List<EmailTemplateDTO> ListMapToDTO(List<EmailTemplate> domains)
        {
            return domains.Select(MapToDTO).ToList();
        }

        public EmailTemplateDTO Map(EmailTemplate source)
        {
            return MapToDTO(source);
        }

        public List<EmailTemplateDTO> MapList(List<EmailTemplate> source)
        {
            return ListMapToDTO(source);
        }
    }
}
