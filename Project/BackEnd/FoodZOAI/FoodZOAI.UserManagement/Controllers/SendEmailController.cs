using FoodZOAI.UserManagement.Contracts;
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodZOAI.UserManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SendEmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly IEmailSettingRepository _emailSettingRepository;
        public SendEmailController(IEmailService emailService, IEmailSettingRepository emailSettingRepository)
        {
            _emailService = emailService;
            _emailSettingRepository = emailSettingRepository;
        }

        [HttpGet("active-mail-settings")]
        public async Task<IActionResult> GetAllActiveEmailSettings()
        {
            var settings = await _emailSettingRepository.GetActiveEmailSettingAsync();
            return Ok(settings);
        }

        [HttpPost("sendEmail")]

        public async Task<IActionResult> SendEmail([FromBody] SendEmailDTO emailDto)
        {
            var result = await _emailService.SendEmailAsync(emailDto);
            if (!result)
                return StatusCode(500, "Email sending failed.");
            return Ok("Email sent successfully.");
        }

    }
}
