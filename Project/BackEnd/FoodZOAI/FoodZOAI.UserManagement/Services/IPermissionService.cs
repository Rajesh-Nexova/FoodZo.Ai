using FoodZOAI.UserManagement.DTOs;

namespace FoodZOAI.UserManagement.Services
{
    public interface IPermissionService
    {
        Task<IEnumerable<PermissionDTO>> GetPermissionsAsync();
        Task<PermissionDTO?> GetPermissionByIdAsync(int id);
        Task AddPermissionAsync(PermissionDTO dto);
        Task UpdatePermissionAsync(int id, PermissionDTO dto);
        Task DeletePermissionAsync(int id);
    }

}
