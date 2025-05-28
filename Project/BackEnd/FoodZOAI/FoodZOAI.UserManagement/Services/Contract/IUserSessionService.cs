using FoodZOAI.UserManagement.DTOs;

namespace FoodZOAI.Services;

public interface IUserSessionService
{
    Task<List<UserSessionDTO>> GetAllAsync();
    Task<UserSessionDTO?> GetByIdAsync(int id);
    Task<UserSessionDTO> AddAsync(UserSessionDTO dto);
    Task<UserSessionDTO> UpdateAsync(UserSessionDTO dto);
    Task<bool> DeleteAsync(int id);
}
