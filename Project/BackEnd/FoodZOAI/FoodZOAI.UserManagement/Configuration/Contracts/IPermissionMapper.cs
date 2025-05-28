using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement.Configuration.Contracts
{
    public interface IPermissionMapper : IMapperServices<Permission, PermissionDTO>
    {
        Permission MapToDomain(PermissionDTO dto);
        List<Permission> ListMapToDomain(List<PermissionDTO> dtoList);
    }
}
