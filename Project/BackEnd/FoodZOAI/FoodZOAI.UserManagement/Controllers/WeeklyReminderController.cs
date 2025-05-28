using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Services;
using FoodZOAI.UserManagement.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodZOAI.UserManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeeklyReminderController : ControllerBase
    {
        private readonly IWeeklyReminderService _service;

        public WeeklyReminderController(IWeeklyReminderService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<WeeklyReminderDTO>>> GetAll()
        {
            var reminders = await _service.GetAllAsync();
            return Ok(reminders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WeeklyReminderDTO>> GetById(int id)
        {
            var reminder = await _service.GetByIdAsync(id);
            if (reminder == null) return NotFound();
            return Ok(reminder);
        }

        [HttpPost]
        public async Task<ActionResult<WeeklyReminderDTO>> Create([FromBody] WeeklyReminderDTO dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<WeeklyReminderDTO>> Update(int id, [FromBody] WeeklyReminderDTO dto)
        {
            if (id != dto.Id) return BadRequest("ID mismatch");

            var existing = await _service.GetByIdAsync(id);
            if (existing == null) return NotFound();

            var updated = await _service.UpdateAsync(dto);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
