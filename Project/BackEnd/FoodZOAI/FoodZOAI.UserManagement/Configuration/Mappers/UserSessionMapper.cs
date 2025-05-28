using FoodZOAI.UserManagement.Models;
using FoodZOAI.UserManagement.DTOs;

namespace FoodZOAI.Mappers;

public class UserSessionMapper : IUserSessionMapper
{
    public UserSession MapToDomain(UserSessionDTO dto)
    {
        return new UserSession
        {
            Id = dto.Id,
            UserId = dto.UserId,
            SessionToken = dto.SessionToken,
            RefreshToken = dto.RefreshToken,
            IpAddress = dto.IpAddress,
            UserAgent = dto.UserAgent,
            DeviceInfo = dto.DeviceInfo,
            Location = dto.Location,
            ExpiresAt = dto.ExpiresAt,
            LastActivityAt = dto.LastActivityAt,
            CreatedAt = dto.CreatedAt,
            UpdatedAt = dto.UpdatedAt,
            DeletedAt = dto.DeletedAt,
            IsActive = dto.IsActive
        };
    }

    public List<UserSession> ListMapToDomain(List<UserSessionDTO> dtos) =>
        dtos.Select(MapToDomain).ToList();
}
