using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement.Contracts
{
    public interface IPermissionRepository
    {
        Task<IEnumerable<Permission>> GetAllAsync();
        Task<Permission?> GetByIdAsync(int id);
        Task AddAsync(Permission permission);
        Task UpdateAsync(Permission permission);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }

}
