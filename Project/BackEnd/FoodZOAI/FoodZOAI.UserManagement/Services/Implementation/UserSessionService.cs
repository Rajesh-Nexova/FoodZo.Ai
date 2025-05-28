using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.Mappers;
using FoodZOAI.Repositories;

namespace FoodZOAI.Services;

public class UserSessionService : IUserSessionService
{
    private readonly IUserSessionRepository _repository;
    private readonly IUserSessionMapper _mapper;

    public UserSessionService(IUserSessionRepository repository, IUserSessionMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<UserSessionDTO>> GetAllAsync()
    {
        var sessions = await _repository.GetAllAsync();
        return sessions.Select(s => new UserSessionDTO
        {
            Id = s.Id,
            UserId = s.UserId,
            SessionToken = s.SessionToken,
            RefreshToken = s.RefreshToken,
            IpAddress = s.IpAddress,
            UserAgent = s.UserAgent,
            DeviceInfo = s.DeviceInfo,
            Location = s.Location,
            ExpiresAt = s.ExpiresAt,
            LastActivityAt = s.LastActivityAt,
            CreatedAt = s.CreatedAt,
            UpdatedAt = s.UpdatedAt,
            DeletedAt = s.DeletedAt,
            IsActive = s.IsActive
        }).ToList();
    }

    public async Task<UserSessionDTO?> GetByIdAsync(int id)
    {
        var s = await _repository.GetByIdAsync(id);
        if (s == null) return null;

        return new UserSessionDTO
        {
            Id = s.Id,
            UserId = s.UserId,
            SessionToken = s.SessionToken,
            RefreshToken = s.RefreshToken,
            IpAddress = s.IpAddress,
            UserAgent = s.UserAgent,
            DeviceInfo = s.DeviceInfo,
            Location = s.Location,
            ExpiresAt = s.ExpiresAt,
            LastActivityAt = s.LastActivityAt,
            CreatedAt = s.CreatedAt,
            UpdatedAt = s.UpdatedAt,
            DeletedAt = s.DeletedAt,
            IsActive = s.IsActive
        };
    }

    public async Task<UserSessionDTO> AddAsync(UserSessionDTO dto)
    {
        var domain = _mapper.MapToDomain(dto);
        var created = await _repository.AddAsync(domain);
        dto.Id = created.Id;
        return dto;
    }

    public async Task<UserSessionDTO> UpdateAsync(UserSessionDTO dto)
    {
        var domain = _mapper.MapToDomain(dto);
        var updated = await _repository.UpdateAsync(domain);
        return dto;
    }

    public async Task<bool> DeleteAsync(int id) =>
        await _repository.DeleteAsync(id);

    Task<List<UserSessionDTO>> IUserSessionService.GetAllAsync()
    {
        throw new NotImplementedException();
    }

    Task<UserSessionDTO?> IUserSessionService.GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}
