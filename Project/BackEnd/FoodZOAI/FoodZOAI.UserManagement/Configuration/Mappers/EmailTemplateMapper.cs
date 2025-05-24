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

        

        

        public EmailTemplateDTO Map(EmailTemplate source)
        {
            if (source == null)
                return new EmailTemplateDTO();

            return new EmailTemplateDTO
            {
                Name = source.Name,
                Subject = source.Subject,
                Body = source.Body,
                IsActive = source.IsActive,
                CreatedByUser = source.CreatedByUser,
                ModifiedByUser = source.ModifiedByUser,
                DeletedByUser = source.DeletedByUser
            };
        }

        public List<EmailTemplateDTO> MapList(List<EmailTemplate> source)
        {
            return source?.Select(Map).ToList() ?? new List<EmailTemplateDTO>();
        }
    }
}
