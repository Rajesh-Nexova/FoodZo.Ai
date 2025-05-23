using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Mappers.Interfaces;
using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement.Mappers
{
    public class UserSessionMapper : IUserSessionMapper
    {
        public UserSessionDTO MapToDTO(UserSession session)
        {
            return new UserSessionDTO
            {
                Id = session.Id,
                UserId = session.UserId,
                SessionToken = session.SessionToken,
                RefreshToken = session.RefreshToken,
                IpAddress = session.IpAddress,
                UserAgent = session.UserAgent,
                DeviceInfo = session.DeviceInfo,
                Location = session.Location,
                ExpiresAt = session.ExpiresAt,
                LastActivityAt = session.LastActivityAt,
                IsActive = session.IsActive
            };
        }

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
                IsActive = dto.IsActive
            };
        }

        public List<UserSessionDTO> MapList(List<UserSession> sessions)
        {
            return sessions.Select(MapToDTO).ToList();
        }
    }
}
