using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement.Configuration.Contracts
{
    public interface IPermissionMapper
    {
        PermissionDTO ToDTO(Permission permission);
        Permission ToEntity(PermissionDTO dto);
    }

}
