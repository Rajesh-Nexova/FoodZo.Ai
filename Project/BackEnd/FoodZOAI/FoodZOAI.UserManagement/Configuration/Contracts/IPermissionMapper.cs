using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement.Configuration.Contracts
{
    public interface IPermissionMapper : IMapperServices<Permission, PermissionDTO>
    {
        PermissionDTO ToDTO(Permission model);
        Permission ToEntity(PermissionDTO dto);
    }
}
