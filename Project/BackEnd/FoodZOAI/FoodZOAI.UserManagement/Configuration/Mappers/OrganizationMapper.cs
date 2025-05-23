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
                SubscriptionPlan=source.SubscriptionPlan,
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

       

        public OrganizationDTO MapToDTO(Organization domain)
        {
            if (domain == null)
                return new OrganizationDTO();

            return new OrganizationDTO
            {
                Id = domain.Id,
                Name = domain.Name,
                Slug = domain.Slug,
                Description = domain.Description,
                Website = domain.Website,
                LogoUrl = domain.LogoUrl,
                SubscriptionPlan = domain.SubscriptionPlan,
                MaxUsers = domain.MaxUsers,
                Status = domain.Status,
                CreatedAt = domain.CreatedAt,
                UpdatedAt = domain.UpdatedAt,

            };
        }

        public List<OrganizationDTO> MapToDTO(List<Organization> domains)
        {
            return domains?.Select(MapToDTO).ToList() ?? new List<OrganizationDTO>();
        }
    }
}
