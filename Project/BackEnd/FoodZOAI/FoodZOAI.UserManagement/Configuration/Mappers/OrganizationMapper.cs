using FoodZOAI.UserManagement.Configuration.Contracts;
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement.Configuration.Mappers
{
    public class OrganizationMapper : IOrganizationMapper
    {
        public OrganizationDTO Map(Organization source)
        {
            if (source == null)
                return new OrganizationDTO();

            return new OrganizationDTO
            {
                Id = source.Id,
                Name = source.Name,
                Slug=source.Slug,
                Description=source.Description,
                Website=source.Website,
                LogoUrl=source.LogoUrl,
                Banner_Image = source.Banner_Image,
                SubscriptionPlan =source.SubscriptionPlan,
                MaxUsers=source.MaxUsers,
                Status=source.Status,
                CreatedAt=source.CreatedAt,
                UpdatedAt=source.UpdatedAt,
                DeletedAt=source.DeletedAt
            };

        }

        public List<OrganizationDTO> MapList(List<Organization> source)
        {
            return source?.Select(Map).ToList() ?? new List<OrganizationDTO>();

        }

       

        

        

        public Organization MapToDomain(OrganizationDTO dto)
        {
            if (dto == null)
                return new Organization();

            return new Organization
            {
                Id = dto.Id,
                Name = dto.Name,
                Slug = dto.Slug,
                Description = dto.Description,
                Website = dto.Website,
                LogoUrl = dto.LogoUrl,
                Banner_Image = dto.Banner_Image,
                SubscriptionPlan = dto.SubscriptionPlan,
                MaxUsers = dto.MaxUsers,
                Status = dto.Status,
                CreatedAt = dto.CreatedAt,
                UpdatedAt = dto.UpdatedAt,

            };
        }

        public List<Organization> MapToDomain(List<OrganizationDTO> domainDtos)
        {
            return domainDtos?.Select(MapToDomain).ToList() ?? new List<Organization>();
        }
    }
}
