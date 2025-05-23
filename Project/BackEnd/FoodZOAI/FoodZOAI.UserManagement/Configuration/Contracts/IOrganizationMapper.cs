using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement.Configuration.Contracts
{
    public interface IOrganizationMapper : IMapperService<Organization, OrganizationDTO>
    {
        /*Organization MapToDomain(OrganizationDTO dto);

        List<Organization> MapToDomain(List<OrganizationDTO> domainDtos);*/
        OrganizationDTO MapToDTO(Organization domain);

        List<OrganizationDTO> MapToDTO(List<Organization> domains);

        
        

        
        

    }
}
