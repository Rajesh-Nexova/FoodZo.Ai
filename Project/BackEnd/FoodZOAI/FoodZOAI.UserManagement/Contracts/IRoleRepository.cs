using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement.Contracts
{
    public interface IRoleRepository
    {
        Task<Role> AddRoleAsync(Role role);
        Task<List<Role>> GetAllAsync();
        Task<Role?> GetByIdAsync(int id);
        Task<bool> DeleteAsync(int id);
        Task<Role> UpdateAsync(Role role);
    }
}
