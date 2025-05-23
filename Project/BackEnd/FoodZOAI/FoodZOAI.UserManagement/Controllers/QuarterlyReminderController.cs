using FoodZOAI.UserManagement.Configuration.Contracts;
using FoodZOAI.UserManagement.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace FoodZOAI.UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuarterlyReminderController : ControllerBase
    {
        private readonly IQuarterlyReminderRepository _quarterlyReminderRepository;
        private readonly IQuarterlyReminderMapper _quarterlyReminderMapper;
        public QuarterlyReminderController(IQuarterlyReminderRepository quarterlyReminderRepository, IQuarterlyReminderMapper quarterlyReminderMapper)
        {
            _quarterlyReminderRepository = quarterlyReminderRepository;
            _quarterlyReminderMapper = quarterlyReminderMapper;

        }

            [HttpGet("GetQuarterlyReminders/{id}")]
            public async Task<IActionResult> GetQuarterlyReminders(int id)
            {
                try
                {
                    var settings = await _quarterlyReminderRepository.GetAllAsync();
                    var result = _quarterlyReminderMapper.MapList(settings.ToList());
                    return Ok(result);
                }
                catch (Exception)
                {
                    return StatusCode(500, "An error occurred while retrieving the GetQuarterlyReminders");
                }
            }
    }
}
