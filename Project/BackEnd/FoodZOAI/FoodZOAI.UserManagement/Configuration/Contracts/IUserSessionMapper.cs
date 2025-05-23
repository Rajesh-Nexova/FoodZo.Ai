
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement.Mappers.Interfaces
{
    public interface IUserSessionMapper
    {
        UserSessionDTO MapToDTO(UserSession session);
        UserSession MapToDomain(UserSessionDTO dto);
        List<UserSessionDTO> MapList(List<UserSession> sessions);
    }
}
