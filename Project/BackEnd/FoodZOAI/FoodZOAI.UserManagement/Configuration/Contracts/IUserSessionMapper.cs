using FoodZOAI.UserManagement.Models;
using FoodZOAI.UserManagement.DTOs;

namespace FoodZOAI.Mappers;

public interface IUserSessionMapper
{
    UserSession MapToDomain(UserSessionDTO dto);
    List<UserSession> ListMapToDomain(List<UserSessionDTO> dtos);
}
