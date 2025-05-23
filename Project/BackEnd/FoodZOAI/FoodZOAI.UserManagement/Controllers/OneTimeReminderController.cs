
using FoodZOAI.UserManagement.Configuration.Contracts;
using FoodZOAI.UserManagement.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace FoodZOAI.UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OneTimeReminderController : ControllerBase
    {
        private readonly IOneTimeReminderRepository _oneTimeReminderRepository;
        private readonly IOneTimeReminderMapper _oneTimeReminderMapper;

        public OneTimeReminderController(
            IOneTimeReminderRepository oneTimeReminderRepository,
            IOneTimeReminderMapper oneTimeReminderMapper)
        {
            _oneTimeReminderRepository = oneTimeReminderRepository;
            _oneTimeReminderMapper = oneTimeReminderMapper;
        }

        [HttpGet("OneTimeReminder/{id}")]
        public async Task<IActionResult> GetOneTimeReminders(int id)
        {
            try
            {
                var reminders = await _oneTimeReminderRepository.GetAllAsync();
                var result = reminders
                    .Select(r => _oneTimeReminderMapper.MapToDTO(r))
                    .ToList();

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while retrieving the OneTime reminders.");
            }
        }
    }
}
