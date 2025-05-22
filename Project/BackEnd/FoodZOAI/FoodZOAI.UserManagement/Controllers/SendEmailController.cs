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
            

            try
            {
                var settings = await _emailSettingRepository.GetDefaultActiveAsync();
                return Ok(settings);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while retrieving the email .");
            }


        }

        
        
        [HttpPost("send-email")]
        public async Task<IActionResult> SendEmail([FromBody] SendEmailDTO dto, [FromQuery] int? smtpSettingId)
        {
            try
            {
                var result = await _emailService.SendEmailAsync(dto, smtpSettingId);

                return Ok(result);
                
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while  the Email sent.");
            }
        }
    }
}
