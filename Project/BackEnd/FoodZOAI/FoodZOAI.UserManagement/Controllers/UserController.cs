using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Services.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodZOAI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> AddUser([FromBody] UserDTO userDto)
    {
        var result = await _userService.AddUserAsync(userDto);
        return Ok(result);
    }

    [HttpGet("GetAllUsers{id}")]
    public async Task<IActionResult> GetAllUsers()
    {
        var result = await _userService.GetAllUsersAsync();
        return Ok(result);
    }

    [HttpGet("getuserbyid{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var result = await _userService.GetUserByIdAsync(id);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDTO userDto)
    {
        var result = await _userService.UpdateUserAsync(id, userDto);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        await _userService.DeleteUserAsync(id);
        return NoContent();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginRequestDTO loginDto)
    {
        var user = await _userService.LoginAsync(loginDto);
        return user == null ? Unauthorized() : Ok(user);
    }

    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestDTO request)
    {
        var success = await _userService.ChangePasswordAsync(request);
        return Ok(success);
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
    {
        var success = await _userService.ResetPasswordAsync(request);
        return Ok(success);
    }

    [HttpPut("profile/{userId}")]
    public async Task<IActionResult> UpdateUserProfile(int userId, [FromBody] UserProfileDTO profileDto)
    {
        var result = await _userService.UpdateUserProfileAsync(userId, profileDto);
        return Ok(result);
    }

    [HttpPost("profile-photo/{userId}")]
    public async Task<IActionResult> UpdateUserProfilePhoto(int userId, IFormFile photo)
    {
        var photoUrl = await _userService.UpdateUserProfilePhotoAsync(userId, photo);
        return photoUrl == null ? NotFound() : Ok(photoUrl);
    }

    [HttpGet("profile/{userId}")]
    public async Task<IActionResult> GetProfile(int userId)
    {
        var result = await _userService.GetProfileAsync(userId);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpGet("paginated")]
    public async Task<IActionResult> GetPaginatedUsers(int pageNumber = 1, int pageSize = 10)
    {
        var (users, totalCount) = await _userService.GetPaginatedUsersAsync(pageNumber, pageSize);
        return Ok(new { Users = users, TotalCount = totalCount });
    }

    [HttpGet("recent")]
    public async Task<IActionResult> GetRecentlyRegisteredUsers([FromQuery] int days = 7)
    {
        var users = await _userService.GetRecentlyRegisteredUsersAsync(days);
        return Ok(users);
    }

    [HttpGet("dropdown")]
    public async Task<IActionResult> GetUsersForDropdown()
    {
        var dropdownList = await _userService.GetUsersForDropdownAsync();
        return Ok(dropdownList);
    }
}
