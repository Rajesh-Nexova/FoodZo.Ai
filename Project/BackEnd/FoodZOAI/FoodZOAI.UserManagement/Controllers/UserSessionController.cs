using Microsoft.AspNetCore.Mvc;
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.Services;

namespace FoodZOAI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserSessionController : ControllerBase
{
    private readonly IUserSessionService _service;

    public UserSessionController(IUserSessionService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(UserSessionDTO dto)
    {
        var created = await _service.AddAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UserSessionDTO dto)
    {
        if (id != dto.Id) return BadRequest();
        var updated = await _service.UpdateAsync(dto);
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
