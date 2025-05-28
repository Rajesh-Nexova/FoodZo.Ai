using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodZOAI.UserManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class YearlyReminderController : ControllerBase
    {
        private readonly IYearlyReminderService _service;

        public YearlyReminderController(IYearlyReminderService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<YearlyReminderDTO>>> GetAll()
        {
            return await _service.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<YearlyReminderDTO>> Get(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return item;
        }

        [HttpPost]
        public async Task<ActionResult<YearlyReminderDTO>> Create(YearlyReminderDTO dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<YearlyReminderDTO>> Update(int id, YearlyReminderDTO dto)
        {
            if (id != dto.Id) return BadRequest();
            return await _service.UpdateAsync(dto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
