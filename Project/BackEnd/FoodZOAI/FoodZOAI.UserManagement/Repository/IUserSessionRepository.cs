using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement.Repositories
{
    public interface IUserSessionRepository
    {
        Task<IEnumerable<UserSession>> GetAllAsync();
        Task<UserSession?> GetByIdAsync(int id);
        Task AddAsync(UserSession session);
        Task UpdateAsync(UserSession session);
        Task DeleteAsync(int id); // Soft delete (set IsActive = false)
    }
}
