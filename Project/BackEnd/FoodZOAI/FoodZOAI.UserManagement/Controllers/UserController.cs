using System.Text;
using FoodZOAI.UserManagement.Configuration.Contracts;
using FoodZOAI.UserManagement.Contracts;
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity.Data;
using FoodZOAI.UserManagement.Utils;



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



        public UserController(
           IUserRepository userRepository,
           IUserProfileRepository userProfileRepository,
           IMapperService<User, UserDTO> userMapper,
           IMapperService<UserProfile, UserProfileDTO> userProfileMapper)
        {
            _userRepository = userRepository;
            _userProfileRepository = userProfileRepository;
            _userMapper = userMapper;
            _userProfileMapper = userProfileMapper;
        }


        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] UserDTO userDto)
        {
            var user = new User
            {
                OrganizationId = userDto.OrganizationId,
                Username = userDto.Username,
                Email = userDto.Email,
                PasswordHash = userDto.PasswordHash,
                Salt = userDto.Salt,
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


        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsersAsync();
            var result = _userMapper.MapList(users.ToList());
            return Ok(result);
        }


        [HttpGet("GetUser/{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            var dto = _userMapper.Map(user);
            return Ok(dto);
        }


        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var users = await _userRepository.GetPaginatedUsersAsync(page, pageSize);
            var result = _userMapper.MapList(users.ToList());
            return Ok(result);
        }


        [HttpPut("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDTO userDto)
        {
            var existingUser = await _userRepository.GetUserByIdAsync(id);
            if (existingUser == null)
                return NotFound();

            existingUser.FirstName = userDto.FirstName;
            existingUser.LastName = userDto.LastName;
            existingUser.Phone = userDto.Phone;
            existingUser.Email = userDto.Email;
            existingUser.UpdatedAt = DateTime.UtcNow;

            var updatedUser = await _userRepository.UpdateUserAsync(existingUser);
            var result = _userMapper.Map(updatedUser);
            return Ok(result);
        }


        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userRepository.DeleteUserAsync(id);
            return NoContent();
        }

        [HttpPost("UserLogin")]
        public async Task<IActionResult> UserLogin([FromBody] UserLoginRequestDTO loginDto)
        {
            var user = await _userRepository.GetUserByUsernameAsync(loginDto.Username);

            if (user == null)
                return Unauthorized("Invalid username or password.");

            // Verify password hash
            var hashedPassword = HashPassword(loginDto.Password, user.Salt);
            if (user.PasswordHash != hashedPassword)
                return Unauthorized("Invalid username or password.");

            // Optional: check account status, locked out, etc.
            if (user.Status == "Locked")
                return Unauthorized("Account is locked.");

            // Update last login timestamp
            user.LastLoginAt = DateTime.UtcNow;
            await _userRepository.UpdateUserAsync(user);

            var userDto = _userMapper.Map(user);
            return Ok(userDto);
        }

        private string HashPassword(string password, string salt)
        {
            using var sha256 = SHA256.Create();
            var combined = Encoding.UTF8.GetBytes(password + salt);
            var hash = sha256.ComputeHash(combined);
            return Convert.ToBase64String(hash);
        }

        /*[HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);

            if (user == null)
                return NotFound("User not found.");

            // Verify current password
            bool currentPasswordValid = PasswordHelper.VerifyPassword(request.CurrentPassword, user.PasswordHash, user.Salt);
            if (!currentPasswordValid)
                return BadRequest("Current password is incorrect.");

            // Hash new password + generate new salt
            var newSalt = PasswordHelper.GenerateSalt();
            var newHash = PasswordHelper.HashPassword(request.NewPassword, newSalt);

            user.PasswordHash = newHash;
            user.Salt = newSalt;
            user.PasswordChangedAt = DateTime.UtcNow;

            await _userRepository.UpdateAsync(user);

            return Ok("Password changed successfully.");
        }

        [HttpPost("ResetPassword")]
    
        public async Task<IActionResult> ResetPassword([FromBody] FoodZOAI.UserManagement.DTOs.ResetPasswordRequest request)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);

            if (user == null)
                return NotFound("User not found.");

            // Continue with password reset logic
            var newSalt = PasswordHelper.GenerateSalt();
            var newHashedPassword = PasswordHelper.HashPassword(request.NewPassword, newSalt);

            user.PasswordHash = newHashedPassword;
            user.Salt = newSalt;
            user.PasswordChangedAt = DateTime.UtcNow;

            await _userRepository.UpdateAsync(user);

            return Ok("Password reset successfully.");
        }*/



    }
}