using FoodZOAI.UserManagement.Configuration.Contracts;
using FoodZOAI.UserManagement.Contracts;
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Services.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserProfileRepository _userProfileRepository;
    private readonly IUserMapper _userMapper;
    private readonly IUserProfileMapper _userProfileMapper;
    private readonly IFileService _fileService;
    private readonly ILogger<UserService> _logger;

    public UserService(
        IUserRepository userRepository,
        IUserProfileRepository userProfileRepository,
        IUserMapper userMapper,
        IUserProfileMapper userProfileMapper,
        IFileService fileService,
        ILogger<UserService> logger)
    {
        _userRepository = userRepository;
        _userProfileRepository = userProfileRepository;
        _userMapper = userMapper;
        _userProfileMapper = userProfileMapper;
        _fileService = fileService;
        _logger = logger;
    }

    public async Task<UserDTO> AddUserAsync(UserDTO userDto)
    {
        var entity = _userMapper.MapToDomain(userDto);
        var created = await _userRepository.AddUserAsync(entity);
        return _userMapper.Map(created);
    }

    public async Task<List<UserDTO>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllUsersAsync();
        return _userMapper.MapList(users.ToList());
    }

    public async Task<UserDTO?> GetUserByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return user == null ? null : _userMapper.Map(user);
    }

    public async Task<UserDTO> UpdateUserAsync(int id, UserDTO userDto)
    {
        var existing = await _userRepository.GetByIdAsync(id);
        if (existing == null) throw new KeyNotFoundException("User not found");

        var updatedEntity = _userMapper.MapToDomain(userDto);
        updatedEntity.Id = id;

        var updated = await _userRepository.UpdateUserAsync(updatedEntity);
        return _userMapper.Map(updated);
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        await _userRepository.DeleteUserAsync(id);
        return true;
    }

    public async Task<UserDTO?> LoginAsync(UserLoginRequestDTO loginDto)
    {
        var user = await _userRepository.GetByCredentialsAsync(loginDto.Username, loginDto.Password);
        return user == null ? null : _userMapper.Map(user);
    }

    public async Task<bool> ChangePasswordAsync(ChangePasswordRequestDTO request)
    {
        return await _userRepository.ChangePasswordAsync(request.UserId, request.CurrentPassword, request.NewPassword);
    }

    public async Task<bool> ResetPasswordAsync(ResetPasswordRequestDTO request)
    {
        return await _userRepository.ResetPasswordAsync(request.UserId, request.NewPassword);
    }

    public async Task<UserProfileDTO> UpdateUserProfileAsync(int userId, UserProfileDTO profileDto)
    {
        var existing = await _userProfileRepository.GetByIdAsync(userId);
        if (existing == null) throw new KeyNotFoundException("Profile not found");

        var domainProfile = _userProfileMapper.MapToDomain(profileDto);
        domainProfile.UserId = userId;

        var updated = await _userProfileRepository.UpdateAsync(domainProfile);
        return _userProfileMapper.Map(updated);
    }

    public async Task<string?> UpdateUserProfilePhotoAsync(int userId, IFormFile photo)
    {
        var profile = await _userProfileRepository.GetByIdAsync(userId);
        if (profile == null) return null;

        var photoUrl = await _fileService.SaveUserProfilePhotoAsync(photo);
        profile.PhotoUrl = photoUrl;

        await _userProfileRepository.UpdateAsync(profile);
        return photoUrl;
    }

    public async Task<UserDTO?> GetProfileAsync(int userId)
    {
        var user = await _userRepository.GetByUserIdAsync(userId);
        return user == null ? null : _userMapper.Map(user);
    }

    public async Task<(List<UserDTO> Users, int TotalCount)> GetPaginatedUsersAsync(int pageNumber, int pageSize)
    {
        var users = await _userRepository.GetPaginatedUsersAsync(pageNumber, pageSize);
        var total = await _userRepository.GetTotalUserCountAsync();
        return (_userMapper.MapList(users.ToList()), total);
    }

    public async Task<List<UserDTO>> GetRecentlyRegisteredUsersAsync(int days)
    {
        var fromDate = DateTime.UtcNow.AddDays(-days);
        var users = await _userRepository.GetUsersRegisteredAfterAsync(fromDate);
        return _userMapper.MapList(users.ToList());
    }



    public Task<List<UserDTO>> GetUsersForDropdownAsync()
    {
        throw new NotImplementedException();
    }
}
