using FoodZOAI.UserManagement.Repositories;
using FoodZOAI.UserManagement.Mappers;
using Microsoft.AspNetCore.Mvc;
using FoodZOAI.UserManagement.Configuration.Contracts;

namespace FoodZOAI.UserManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeeklyReminderController : ControllerBase
    {
        private readonly IWeeklyReminderRepository _repository;
        private readonly IWeeklyReminderMapper _mapper;

        public WeeklyReminderController(IWeeklyReminderRepository repository, IWeeklyReminderMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //[HttpGet("GetWeeklyReminders")]
        //public async Task<IActionResult> GetWeeklyReminders()
        //{
        //    var reminders = await _repository.GetAllAsync();
        //    var result = reminders.Select(r => _mapper.MapToDTO(r)).ToList();
        //    return Ok(result);
        //}

        [HttpGet("GetWeeklyReminders/{id}")]
        public async Task<IActionResult> GetWeeklyReminderById(int id)
        {
            var reminder = await _repository.GetByIdAsync(id);
            if (reminder is null)
                return NotFound("Weekly reminder not found.");

            var result = _mapper.MapToDTO(reminder);
            return Ok(result);
        }
    }
}
