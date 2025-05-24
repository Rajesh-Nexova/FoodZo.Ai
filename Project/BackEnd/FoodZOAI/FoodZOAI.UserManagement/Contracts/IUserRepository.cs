using FoodZOAI.UserManagement.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodZOAI.UserManagement.Contracts
{
    public interface IUserRepository
    {
        Task<User> AddUserAsync(User user);
        Task<User?> GetUserByIdAsync(int id);
        Task<User?> GetByIdAsync(int id); // Consider merging this with GetUserByIdAsync for clarity
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<IEnumerable<User>> GetPaginatedUsersAsync(int pageNumber, int pageSize);
        Task<int> GetTotalUserCountAsync();
        Task<IEnumerable<User>> GetUsersRegisteredAfterAsync(DateTime fromDate);
        Task<IEnumerable<User>> GetRecentlyRegisteredUsersAsync(int count); // Not used in controller – consider removing
        Task<IEnumerable<User>> GetUsersOrderedByCreationDateAsync(int count); // Not used – consider removing

        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> GetUserByUsernameAsync(string username);

        Task<User> UpdateUserAsync(User user);
        Task UpdateAsync(User user); // `object` changed to `User`

        Task DeleteUserAsync(int id);

        Task<bool> ChangePasswordAsync(int userId, string newPasswordHash, string newSalt); // Optional – not used in controller

        Task<User?> GetByUserIdAsync(int userId); // Used in GetProfile
    }
}
