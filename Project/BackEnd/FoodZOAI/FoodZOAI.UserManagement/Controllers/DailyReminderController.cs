using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Services.Contracts;
using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DailyReminderController : ControllerBase
    {
        private readonly IDailyReminderService _service;

        public DailyReminderController(IDailyReminderService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<DailyReminderDTO>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DailyReminderDTO>> GetById(int id)
        {
            var reminder = await _service.GetByIdAsync(id);
            if (reminder == null) return NotFound();
            return Ok(reminder);
        }

        [HttpPost]
        public async Task<ActionResult<DailyReminderDTO>> Create(DailyReminderDTO dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DailyReminderDTO>> Update(int id, DailyReminderDTO dto)
        {
            if (id != dto.Id) return BadRequest("ID mismatch");
            var updated = await _service.UpdateAsync(dto);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
