using FoodZOAI.UserManagement.DTOs;

namespace FoodZOAI.UserManagement.Services.Contract
{
    public interface IRoleService
    {
        Task<List<RoleDTO>> GetAllRolesAsync();
        Task<RoleDTO> UpdateRoleAsync(RoleDTO role);
        Task<bool> DeleteRoleAsync(int id);
    }
}
