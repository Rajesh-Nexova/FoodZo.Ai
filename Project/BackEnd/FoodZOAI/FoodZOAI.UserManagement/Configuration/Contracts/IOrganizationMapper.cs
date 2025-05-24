using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement.Configuration.Contracts
{
    public interface IOrganizationMapper : IMapperServices<Organization, OrganizationDTO>
    {
        Organization MapToDomain(OrganizationDTO dto);

        List<Organization> MapToDomain(List<OrganizationDTO> domainDtos);








    }
}
