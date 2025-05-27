// IUserRepository.cs
using FoodZOAI.UserManagement.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodZOAI.UserManagement.Contracts
{
    public interface IUserRepository
    {
        Task<User> AddUserAsync(User user);
        Task<User?> GetByIdAsync(int id);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<IEnumerable<User>> GetPaginatedUsersAsync(int pageNumber, int pageSize);
        Task<int> GetTotalUserCountAsync();
        Task<IEnumerable<User>> GetUsersRegisteredAfterAsync(DateTime fromDate);
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> GetUserByUsernameAsync(string username);
        Task<User?> GetByCredentialsAsync(string username, string passwordHash);
        Task<User?> GetByUserIdAsync(int userId);
        Task<IEnumerable<User>> GetUsersForDropdownAsync();


        Task<User> UpdateUserAsync(User user);
        Task UpdateAsync(User user);
        Task<bool> ChangePasswordAsync(int userId, string newPasswordHash, string newSalt);
        Task<bool> ResetPasswordAsync(int userId, string newPasswordHash);
        Task DeleteUserAsync(int id);
    }
}
