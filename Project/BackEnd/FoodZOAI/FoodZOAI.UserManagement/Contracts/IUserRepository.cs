using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement.Contracts
{
    public interface IUserRepository
    {
        Task<User> AddUserAsync(User user);
        Task<User?> GetUserByIdAsync(int id);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<IEnumerable<User>> GetPaginatedUsersAsync(int pageNumber, int pageSize);
        Task<IEnumerable<User>> GetRecentlyRegisteredUsersAsync(int count);
        Task<User?> GetUserByEmailAsync(string email);
        Task<User> UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
        Task<bool> ChangePasswordAsync(int userId, string newPasswordHash, string newSalt);
        Task<User?> GetUserByUsernameAsync(string username);
        Task GetByIdAsync(object userId);
        Task UpdateAsync(object user);
    }
}
