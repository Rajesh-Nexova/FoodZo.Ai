using FoodZOAI.UserManagement.DTOs;

namespace FoodZOAI.UserManagement.Services
{
    public interface IUserService
    {
        Task<UserDTO> AddUserAsync(UserDTO userDto);
        Task<UserDTO?> GetUserByIdAsync(int id);
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task<IEnumerable<UserDTO>> GetPaginatedUsersAsync(int pageNumber, int pageSize);
        Task<UserDTO?> GetUserByEmailAsync(string email);
        Task<UserDTO?> GetUserByUsernameAsync(string username);
        Task<UserDTO> UpdateUserAsync(UserDTO userDto);
        Task DeleteUserAsync(int id);
        Task<bool> ChangePasswordAsync(int userId, string newPasswordHash, string newSalt);
        Task<IEnumerable<UserDTO>> GetRecentlyRegisteredUsersAsync(int count);
        Task<int> GetTotalUserCountAsync();
        Task<IEnumerable<UserDTO>> GetUsersRegisteredAfterAsync(DateTime fromDate);
    }
}
