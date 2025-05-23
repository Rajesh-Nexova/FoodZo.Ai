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

        //[HttpGet("GetMonthlyReminders")]
        //public async Task<IActionResult> GetMonthlyReminders()
        //{
        //    var result = await _service.GetMonthlyRemindersAsync();
        //    return Ok(result);
        //}

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMonthlyReminderById(int id)
        {
            var reminder = await _service.GetMonthlyReminderByIdAsync(id);
            if (reminder == null)
            {
                return NotFound($"Monthly reminder with ID {id} was not found.");
            }
            return Ok(reminder);
        }
    }
}
