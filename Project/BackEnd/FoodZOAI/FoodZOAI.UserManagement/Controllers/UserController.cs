using FoodZOAI.UserManagement.Configuration.Contracts;
using FoodZOAI.UserManagement.Contracts;
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;
using Microsoft.AspNetCore.Mvc;
using FoodZOAI.UserManagement.Utils;
using FoodZOAI.UserManagement.Services;

namespace FoodZOAI.UserManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IMapperService<User, UserDTO> _userMapper;
        private readonly IMapperService<UserProfile, UserProfileDTO> _userProfileMapper;
        private readonly IFileService _fileService;

        public UserController(
            IUserRepository userRepository,
            IUserProfileRepository userProfileRepository,
            IMapperService<User, UserDTO> userMapper,
            IMapperService<UserProfile, UserProfileDTO> userProfileMapper,
            IFileService fileService)
        {
            _userRepository = userRepository;
            _userProfileRepository = userProfileRepository;
            _userMapper = userMapper;
            _userProfileMapper = userProfileMapper;
            _fileService = fileService;
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] UserDTO userDto)
        {
            try
            {
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
                var resultDto = _userMapper.Map(createdUser);
                return Ok(resultDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userRepository.GetAllUsersAsync();
                var result = _userMapper.MapList(users.ToList());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetUser/{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(id);
                if (user == null)
                    return NotFound();

                var result = _userMapper.Map(user);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDTO userDto)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(id);
                if (user == null)
                    return NotFound();

                user.FirstName = userDto.FirstName;
                user.LastName = userDto.LastName;
                user.Email = userDto.Email;
                user.Phone = userDto.Phone;
                user.Status = userDto.Status;
                user.UpdatedAt = DateTime.UtcNow;

                var updatedUser = await _userRepository.UpdateUserAsync(user);
                var result = _userMapper.Map(updatedUser);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await _userRepository.DeleteUserAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("UserLogin")]
        public async Task<IActionResult> UserLogin([FromBody] UserLoginRequestDTO loginDto)
        {
            try
            {
                var user = await _userRepository.GetUserByUsernameAsync(loginDto.Username);
                if (user == null || !PasswordHelper.VerifyPassword(loginDto.Password, user.PasswordHash, user.Salt))
                    return Unauthorized("Invalid credentials.");

                if (user.Status == "Locked")
                    return Unauthorized("Account is locked.");

                user.LastLoginAt = DateTime.UtcNow;
                await _userRepository.UpdateUserAsync(user);

                var userDto = _userMapper.Map(user);
                return Ok(userDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(request.UserId);
                if (user == null)
                    return NotFound("User not found.");

                if (!PasswordHelper.VerifyPassword(request.CurrentPassword, user.PasswordHash, user.Salt))
                    return BadRequest("Incorrect current password.");

                var newSalt = PasswordHelper.GenerateSalt();
                var newHash = PasswordHelper.HashPassword(request.NewPassword, newSalt);

                user.Salt = newSalt;
                user.PasswordHash = newHash;
                user.PasswordChangedAt = DateTime.UtcNow;

                await _userRepository.UpdateAsync(user);
                return Ok("Password changed successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(request.UserId);
                if (user == null)
                    return NotFound("User not found.");

                var newSalt = PasswordHelper.GenerateSalt();
                var newHash = PasswordHelper.HashPassword(request.NewPassword, newSalt);

                user.Salt = newSalt;
                user.PasswordHash = newHash;
                user.PasswordChangedAt = DateTime.UtcNow;

                await _userRepository.UpdateAsync(user);
                return Ok("Password reset successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("UpdateUserProfile/{id}")]
        public async Task<IActionResult> UpdateUserProfile([FromBody] UserProfileDTO profileDto, int id)
        {
            try
            {
                var profile = await _userProfileRepository.GetByIdAsync(id);
                if (profile == null)
                    return NotFound("User profile not found.");

                var updatedProfile = _userProfileMapper.MapToEntity(profileDto, profile);
                await _userProfileRepository.UpdateAsync(updatedProfile);

                var result = _userProfileMapper.Map(updatedProfile);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("UpdateUserProfilePhoto")]
        public async Task<IActionResult> UpdateUserProfilePhoto(int userId, IFormFile photo)
        {
            if (photo == null || photo.Length == 0)
                return BadRequest("Invalid photo file.");

            var photoUrl = await _fileService.SaveUserProfilePhotoAsync(userId, photo);

            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                return NotFound("User not found.");

            user.AvatarUrl = photoUrl;
            await _userRepository.UpdateUserAsync(user);

            return Ok(new { PhotoUrl = photoUrl });
        }


        [HttpGet("GetProfile")]
        public async Task<IActionResult> GetProfile([FromQuery] int userId)
        {
            try
            {
                var profile = await _userRepository.GetByUserIdAsync(userId);
                if (profile == null)
                    return NotFound("User profile not found.");

                var result = _userMapper.Map(profile);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetUserForDropdown")]
        public async Task<IActionResult> GetUserForDropdown()
        {
            try
            {
                var users = await _userRepository.GetAllUsersAsync();
                var result = users
                    .Where(u => u.DeletedAt == null)
                    .Select(u => new
                    {
                        u.Id,
                        Name = $"{u.FirstName} {u.LastName}"
                    })
                    .ToList();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET: api/User/GetUsers?pageNumber=1&pageSize=10
        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
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
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET: api/User/GetRecentlyRegisteredUsers?days=7
        [HttpGet("GetRecentlyRegisteredUsers")]
        public async Task<IActionResult> GetRecentlyRegisteredUsers([FromQuery] int days = 7)
        {
            try
            {
                var fromDate = DateTime.UtcNow.AddDays(-days);
                var recentUsers = await _userRepository.GetUsersRegisteredAfterAsync(fromDate);
                var result = _userMapper.MapList(recentUsers.ToList());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
