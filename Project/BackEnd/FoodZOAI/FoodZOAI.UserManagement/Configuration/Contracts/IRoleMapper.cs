using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement.Configuration.Contracts
{
    public interface IRoleMapper : IMapperServices<Role, RoleDTO>
    {
        Role MapToDomain(RoleDTO dto);
        List<Role> ListMapToDomain(List<RoleDTO> dtoList);
        Role MapToEntity(RoleDTO roleDto, Role existingRole);
    }
}
