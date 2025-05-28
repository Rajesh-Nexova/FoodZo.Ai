using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.Repositories;

public interface IUserSessionRepository
{
    Task<List<UserSession>> GetAllAsync();
    Task<UserSession?> GetByIdAsync(int id);
    Task<UserSession> AddAsync(UserSession session);
    Task<UserSession> UpdateAsync(UserSession session);
    Task<bool> DeleteAsync(int id);
}
