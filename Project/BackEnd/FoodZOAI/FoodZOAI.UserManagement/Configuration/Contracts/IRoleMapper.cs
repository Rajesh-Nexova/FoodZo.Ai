using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement.Configuration.Contracts
{
    public interface IRoleMapper
    {
        Role MapToDomain(RoleDTO roleDto);  // correct
        RoleDTO MapToDTO(Role role);
    }
}
