using FoodZOAI.UserManagement.Configuration.Contracts;
using FoodZOAI.UserManagement.Contracts;
using FoodZOAI.UserManagement.Mappers;
using FoodZOAI.UserManagement.Mappers.Interfaces;
using FoodZOAI.UserManagement.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodZOAI.UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YearlyReminderController : ControllerBase
    {
        private readonly IYearlyReminderRepository _yearlyReminderRepository;
        private readonly IYearlyReminderMapper _yearlyReminderMapper;

        public YearlyReminderController(
            IYearlyReminderRepository yearlyReminderRepository,
            IYearlyReminderMapper yearlyReminderMapper)
        {
            _yearlyReminderRepository = yearlyReminderRepository;
            _yearlyReminderMapper = yearlyReminderMapper;
        }

        [HttpGet("YearlyReminder/{id}")]
        public async Task<IActionResult> GetYearlyReminders(int id)
        {
            try
            {
                var reminders = await _yearlyReminderRepository.GetAllAsync();
                var result = reminders.Select(r => _yearlyReminderMapper.MapToDTO(r)).ToList();
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while retrieving the YearlyReminder");
            }
        }
    }
}
