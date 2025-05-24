using FoodZOAI.UserManagement.Configuration.Contracts;
using FoodZOAI.UserManagement.Contracts;
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;
using FoodZOAI.UserManagement.Services;
using FoodZOAI.UserManagement.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FoodZOAI.UserManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IMapperServices<User, UserDTO> _userMapper;
        private readonly IMapperServices<UserProfile, UserProfileDTO> _userProfileMapper;
        private readonly IFileService _fileService;
        private readonly ILogger<UserController> _logger;

        public UserController(
            IUserRepository userRepository,
            IUserProfileRepository userProfileRepository,
            IMapperServices<User, UserDTO> userMapper,
            IMapperServices<UserProfile, UserProfileDTO> userProfileMapper,
            IFileService fileService,
            ILogger<UserController> logger)
        {
            _userRepository = userRepository;
            _userProfileRepository = userProfileRepository;
            _userMapper = userMapper;
            _userProfileMapper = userProfileMapper;
            _fileService = fileService;
            _logger = logger;
        }

        // ────────────────────────────────────────────────────────────────
        //  User CRUD
        // ────────────────────────────────────────────────────────────────

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] UserDTO userDto)
        {
            _logger.LogInformation("AddUser called for Username: {Username}", userDto.Username);

            var salt = PasswordHelper.GenerateSalt();
            var hashedPassword = PasswordHelper.HashPassword(userDto.PasswordHash, salt);

            var user = new User
            {
                OrganizationId = userDto.OrganizationId,
                Username = userDto.Username,
                Email = userDto.Email,
                PasswordHash = hashedPassword,
                Salt = salt,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Phone = userDto.Phone,
                AvatarUrl = userDto.AvatarUrl,
                Status = userDto.Status,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = userDto.CreatedBy
            };

            var createdUser = await _userRepository.AddUserAsync(user);
            _logger.LogInformation("User created with Id: {UserId}", createdUser.Id);

            return Ok(_userMapper.Map(createdUser));
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            _logger.LogInformation("GetAllUsers called");

            var users = await _userRepository.GetAllUsersAsync();
            var result = _userMapper.MapList(users.ToList());

            _logger.LogInformation("GetAllUsers returned {Count} users", result.Count);
            return Ok(result);
        }

        [HttpGet("GetUser/{id:int}")]
        public async Task<IActionResult> GetUser(int id)
        {
            _logger.LogInformation("GetUser called for Id: {Id}", id);

            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                _logger.LogWarning("GetUser: user with Id {Id} not found", id);
                return NotFound();
            }

            return Ok(_userMapper.Map(user));
        }

        [HttpPut("UpdateUser/{id:int}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDTO userDto)
        {
            _logger.LogInformation("UpdateUser called for Id: {Id}", id);

            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                _logger.LogWarning("UpdateUser: user with Id {Id} not found", id);
                return NotFound();
            }

            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.Email = userDto.Email;
            user.Phone = userDto.Phone;
            user.Status = userDto.Status;
            user.UpdatedAt = DateTime.UtcNow;

            var updatedUser = await _userRepository.UpdateUserAsync(user);
            _logger.LogInformation("UpdateUser succeeded for Id: {Id}", id);

            return Ok(_userMapper.Map(updatedUser));
        }

        [HttpDelete("DeleteUser/{id:int}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            _logger.LogInformation("DeleteUser called for Id: {Id}", id);

            await _userRepository.DeleteUserAsync(id);
            _logger.LogInformation("DeleteUser succeeded for Id: {Id}", id);

            return NoContent();
        }

        // ────────────────────────────────────────────────────────────────
        //  Authentication & Password
        // ────────────────────────────────────────────────────────────────

        [HttpPost("UserLogin")]
        public async Task<IActionResult> UserLogin([FromBody] UserLoginRequestDTO loginDto)
        {
            _logger.LogInformation("UserLogin attempt for Username: {Username}", loginDto.Username);

            var user = await _userRepository.GetUserByUsernameAsync(loginDto.Username);
            if (user == null || !PasswordHelper.VerifyPassword(loginDto.Password, user.PasswordHash, user.Salt))
            {
                _logger.LogWarning("UserLogin failed for Username: {Username}", loginDto.Username);
                return Unauthorized("Invalid credentials.");
            }

            if (user.Status == "Locked")
            {
                _logger.LogWarning("UserLogin failed (Locked) for Username: {Username}", loginDto.Username);
                return Unauthorized("Account is locked.");
            }

            user.LastLoginAt = DateTime.UtcNow;
            await _userRepository.UpdateUserAsync(user);

            _logger.LogInformation("UserLogin successful for Username: {Username}", loginDto.Username);
            return Ok(_userMapper.Map(user));
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            _logger.LogInformation("ChangePassword called for UserId: {Id}", request.UserId);

            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null)
            {
                _logger.LogWarning("ChangePassword: user with Id {Id} not found", request.UserId);
                return NotFound("User not found.");
            }

            if (!PasswordHelper.VerifyPassword(request.CurrentPassword, user.PasswordHash, user.Salt))
            {
                _logger.LogWarning("ChangePassword failed: incorrect current password for UserId {Id}", request.UserId);
                return BadRequest("Incorrect current password.");
            }

            var newSalt = PasswordHelper.GenerateSalt();
            var newHash = PasswordHelper.HashPassword(request.NewPassword, newSalt);

            user.Salt = newSalt;
            user.PasswordHash = newHash;
            user.PasswordChangedAt = DateTime.UtcNow;

            await _userRepository.UpdateAsync(user);
            _logger.LogInformation("ChangePassword succeeded for UserId: {Id}", request.UserId);

            return Ok("Password changed successfully.");
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            _logger.LogInformation("ResetPassword called for UserId: {Id}", request.UserId);

            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null)
            {
                _logger.LogWarning("ResetPassword: user with Id {Id} not found", request.UserId);
                return NotFound("User not found.");
            }

            var newSalt = PasswordHelper.GenerateSalt();
            var newHash = PasswordHelper.HashPassword(request.NewPassword, newSalt);

            user.Salt = newSalt;
            user.PasswordHash = newHash;
            user.PasswordChangedAt = DateTime.UtcNow;

            await _userRepository.UpdateAsync(user);
            _logger.LogInformation("ResetPassword succeeded for UserId: {Id}", request.UserId);

            return Ok("Password reset successfully.");
        }

        // ────────────────────────────────────────────────────────────────
        //  Profiles & Photos
        // ────────────────────────────────────────────────────────────────

        [HttpPut("UpdateUserProfile/{id:int}")]
        public async Task<IActionResult> UpdateUserProfile([FromBody] UserProfileDTO profileDto, int id)
        {
            _logger.LogInformation("UpdateUserProfile called for ProfileId: {Id}", id);

            var profile = await _userProfileRepository.GetByIdAsync(id);
            if (profile == null)
            {
                _logger.LogWarning("UpdateUserProfile: profile with Id {Id} not found", id);
                return NotFound("User profile not found.");
            }

            var updatedProfile = _userProfileMapper.MapToDomain(profileDto, profile);
            await _userProfileRepository.UpdateAsync(updatedProfile);

            _logger.LogInformation("UpdateUserProfile succeeded for ProfileId: {Id}", id);
            return Ok(_userProfileMapper.Map(updatedProfile));
        }

        [HttpPost("UpdateUserProfilePhoto")]
        public async Task<IActionResult> UpdateUserProfilePhoto(int userId, IFormFile photo)
        {
            _logger.LogInformation("UpdateUserProfilePhoto called for UserId: {Id}", userId);

            if (photo == null || photo.Length == 0)
            {
                _logger.LogWarning("UpdateUserProfilePhoto failed: invalid photo for UserId: {Id}", userId);
                return BadRequest("Invalid photo file.");
            }

            var photoUrl = await _fileService.SaveUserProfilePhotoAsync(userId, photo);

            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                _logger.LogWarning("UpdateUserProfilePhoto: user with Id {Id} not found", userId);
                return NotFound("User not found.");
            }

            user.AvatarUrl = photoUrl;
            await _userRepository.UpdateUserAsync(user);

            _logger.LogInformation("UpdateUserProfilePhoto succeeded for UserId: {Id}", userId);
            return Ok(new { PhotoUrl = photoUrl });
        }

        [HttpGet("GetProfile")]
        public async Task<IActionResult> GetProfile([FromQuery] int userId)
        {
            _logger.LogInformation("GetProfile called for UserId: {Id}", userId);

            var profile = await _userRepository.GetByUserIdAsync(userId);
            if (profile == null)
            {
                _logger.LogWarning("GetProfile: profile not found for UserId {Id}", userId);
                return NotFound("User profile not found.");
            }

            return Ok(_userMapper.Map(profile));
        }

        // ────────────────────────────────────────────────────────────────
        //  Lists & Queries
        // ────────────────────────────────────────────────────────────────

        [HttpGet("GetUserForDropdown")]
        public async Task<IActionResult> GetUserForDropdown()
        {
            _logger.LogInformation("GetUserForDropdown called");

            var users = await _userRepository.GetAllUsersAsync();
            var result = users
                .Where(u => u.DeletedAt == null)
                .Select(u => new
                {
                    u.Id,
                    Name = $"{u.FirstName} {u.LastName}"
                })
                .ToList();

            _logger.LogInformation("GetUserForDropdown returned {Count} entries", result.Count);
            return Ok(result);
        }

        // GET: api/User/GetUsers?pageNumber=1&pageSize=10
        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            _logger.LogInformation("GetUsers called: pageNumber={PageNumber}, pageSize={PageSize}", pageNumber, pageSize);

            var users = await _userRepository.GetPaginatedUsersAsync(pageNumber, pageSize);
            var totalCount = await _userRepository.GetTotalUserCountAsync();

            var result = _userMapper.MapList(users.ToList());

            return Ok(new
            {
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Users = result
            });
        }

        // GET: api/User/GetRecentlyRegisteredUsers?days=7
        [HttpGet("GetRecentlyRegisteredUsers")]
        public async Task<IActionResult> GetRecentlyRegisteredUsers([FromQuery] int days = 7)
        {
            _logger.LogInformation("GetRecentlyRegisteredUsers called: days={Days}", days);

            var fromDate = DateTime.UtcNow.AddDays(-days);
            var recentUsers = await _userRepository.GetUsersRegisteredAfterAsync(fromDate);
            var result = _userMapper.MapList(recentUsers.ToList());

            _logger.LogInformation("GetRecentlyRegisteredUsers returned {Count} users", result.Count);
            return Ok(result);
        }
    }
}
