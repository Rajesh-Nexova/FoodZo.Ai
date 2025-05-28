using FoodZOAI.UserManagement.DTOs;
using Microsoft.AspNetCore.Http;

public interface IUserService
{
    Task<UserDTO> AddUserAsync(UserDTO userDto);
    Task<List<UserDTO>> GetAllUsersAsync();
    Task<UserDTO?> GetUserByIdAsync(int id);
    Task<UserDTO> UpdateUserAsync(int id, UserDTO userDto);
    Task<bool> DeleteUserAsync(int id);
    Task<UserDTO?> LoginAsync(UserLoginRequestDTO loginDto);
    Task<bool> ChangePasswordAsync(ChangePasswordRequest request);
    Task<bool> ResetPasswordAsync(ResetPasswordRequest request);
    Task<UserProfileDTO> UpdateUserProfileAsync(int userId, UserProfileDTO profileDto);
    Task<string?> UpdateUserProfilePhotoAsync(int userId, IFormFile photo);
    Task<UserDTO?> GetProfileAsync(int userId);
    Task<(List<UserDTO> Users, int TotalCount)> GetPaginatedUsersAsync(int pageNumber, int pageSize);
    Task<List<UserDTO>> GetRecentlyRegisteredUsersAsync(int days);
    Task<List<UserDTO>> GetUsersForDropdownAsync();
}
