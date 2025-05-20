using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement.Contracts
{
    public interface IUserProfileRepository
    {
        Task<UserProfile?> GetByUserIdAsync(int userId);
        Task<UserProfile> AddAsync(UserProfile profile);
        Task<UserProfile> UpdateAsync(UserProfile profile);
        Task DeleteAsync(int userId);
    }
}
