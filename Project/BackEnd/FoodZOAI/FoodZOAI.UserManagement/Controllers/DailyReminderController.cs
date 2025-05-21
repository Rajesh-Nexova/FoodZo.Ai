using FoodZOAI.UserManagement.Configuration.Contracts;
using FoodZOAI.UserManagement.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodZOAI.UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DailyReminderController : ControllerBase
    {
        private readonly IDailyReminderRepository _dailyReminderRepository;
        private readonly IDailyReminderMapper _dailyReminderMapper;
        public DailyReminderController(IDailyReminderRepository dailyReminderRepository, IDailyReminderMapper dailyReminderMapper)
        {
            _dailyReminderRepository = dailyReminderRepository;
            _dailyReminderMapper= dailyReminderMapper;

        }

        //[HttpGet("GetDailyReminders")]
        //public async Task<IActionResult> GetDailyReminders()
        //{
        //    try
        //    {
        //        var settings = await _dailyReminderRepository.GetAllAsync();
        //        var result = _dailyReminderMapper.MapList(settings.ToList());
        //        return Ok(result);
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(500, "An error occurred while retrieving the GetDailyReminders.");
        //    }
        //}

        [HttpGet("GetDailyReminders/{id}")]
        public async Task<IActionResult> GetDailyRemindersById(int id)
        {
            try
            {
                var setting = await _dailyReminderRepository.GetByIdAsync(id);
                if (setting == null)
                    return NotFound("GetDaily Reminders not found.");

                var result = _dailyReminderMapper.MapToDTO(setting);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while retrieving the GetDaily Reminders.");
            }
        }



    }
}
