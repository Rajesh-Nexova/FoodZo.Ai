using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Services.Contracts;

namespace FoodZOAI.UserManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HalfYearlyReminderController : ControllerBase
    {
        private readonly IHalfYearlyReminderService _service;

        public HalfYearlyReminderController(IHalfYearlyReminderService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<HalfYearlyReminderDTO>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HalfYearlyReminderDTO>> GetById(int id)
        {
            var reminder = await _service.GetByIdAsync(id);
            if (reminder == null) return NotFound();
            return Ok(reminder);
        }

        [HttpPost]
        public async Task<ActionResult<HalfYearlyReminderDTO>> Create(HalfYearlyReminderDTO dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<HalfYearlyReminderDTO>> Update(int id, HalfYearlyReminderDTO dto)
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
