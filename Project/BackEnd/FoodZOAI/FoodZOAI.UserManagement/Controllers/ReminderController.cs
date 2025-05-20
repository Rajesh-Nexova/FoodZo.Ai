using FoodZOAI.UserManagement.Models;
using FoodZOAI.UserManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodZOAI.UserManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReminderController : ControllerBase
    {
        private readonly IReminderService _reminderService;

        public ReminderController(IReminderService reminderService)
        {
            _reminderService = reminderService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var reminders = await _reminderService.GetAllActiveAsync();
            return Ok(reminders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var reminder = await _reminderService.GetByIdAsync(id);
            if (reminder == null)
                return NotFound();

            return Ok(reminder);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] Reminder reminder)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _reminderService.CreateAsync(reminder);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Reminder reminder)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _reminderService.UpdateAsync(id, reminder);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _reminderService.DeleteAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
