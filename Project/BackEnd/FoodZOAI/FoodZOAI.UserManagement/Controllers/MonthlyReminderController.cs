using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Services;
using FoodZOAI.UserManagement.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace FoodZOAI.UserManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MonthlyReminderController : ControllerBase
    {
        private readonly IMonthlyReminderService _service;

        public MonthlyReminderController(IMonthlyReminderService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<MonthlyReminderDTO>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MonthlyReminderDTO>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<MonthlyReminderDTO>> Create(MonthlyReminderDTO dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut]
        public async Task<ActionResult<MonthlyReminderDTO>> Update(MonthlyReminderDTO dto)
        {
            var updated = await _service.UpdateAsync(dto);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}
