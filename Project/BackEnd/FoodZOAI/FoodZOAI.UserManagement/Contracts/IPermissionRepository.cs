// File: IPermissionRepository.cs
using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement.Configuration.Contracts
{
    public interface IPermissionRepository
    {
        Task<List<Permission>> GetAllAsync();
        Task<Permission?> GetByIdAsync(int id);
        Task<Permission> AddAsync(Permission permission);
        Task<Permission> UpdateAsync(Permission permission);
        Task<bool> DeleteAsync(int id);
    }
}
