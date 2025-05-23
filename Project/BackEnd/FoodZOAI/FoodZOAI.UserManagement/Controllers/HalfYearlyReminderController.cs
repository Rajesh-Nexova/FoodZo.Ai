using FoodZOAI.UserManagement.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace FoodZOAI.UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HalfYearlyReminderController : ControllerBase
    {
        private readonly IHalfYearlyReminderRepository _halfYearlyReminderRepository;
        private readonly IHalfYearlyReminderMapper _halfYearlyReminderMapper;
        public HalfYearlyReminderController(IHalfYearlyReminderRepository halfYearlyReminderRepository, IHalfYearlyReminderMapper halfYearlyReminderMapper)
        {
            _halfYearlyReminderRepository = halfYearlyReminderRepository;
            _halfYearlyReminderMapper = halfYearlyReminderMapper;

        }


        [HttpGet("HalfYearlyReminder/{id}")]
        public async Task<IActionResult> GetHalfYearlyReminders(int id)
        {
            try
            {
                var settings = await _halfYearlyReminderRepository.GetAllAsync();
                var result = _halfYearlyReminderMapper.MapList(settings.ToList());
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while retrieving the HalfYearlyReminder");
            }
        }
    }
}
