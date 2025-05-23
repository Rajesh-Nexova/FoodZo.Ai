using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("GetActiveUserCount")]
    public async Task<IActionResult> GetActiveUserCount()
    {
        var count = await _userService.GetActiveUserCountAsync();
        return Ok(new { ActiveUserCount = count });
    }


    [HttpGet("GetInactiveUserCount")]
    public async Task<IActionResult> GetInactiveUserCount()
    {
        var count = await _userService.GetInactiveUserCountAsync();
        return Ok(new { inactiveUserCount = count });
    }


    [HttpGet("GetTotalUserCount")]
    public async Task<IActionResult> GetTotalUserCount()
    {
        var count = await _userService.GetTotalUserCountAsync();
        return Ok(new { totalUserCount = count });
    }

    [HttpGet("GetOnlineUsers")]
    public async Task<IActionResult> GetOnlineUsers()
    {
        var users = await _userService.GetOnlineUsersAsync();
        return Ok(users);
    }
}
