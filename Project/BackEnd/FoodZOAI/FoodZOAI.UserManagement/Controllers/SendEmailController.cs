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
        private readonly ILogger<SendEmailController> _logger;
        public SendEmailController(IEmailService emailService, IEmailSettingRepository emailSettingRepository, ILogger<SendEmailController> logger)
        {
            _emailService = emailService;
            _emailSettingRepository = emailSettingRepository;
            _logger = logger;
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
        public async Task<IActionResult> SendEmail([FromForm] SendEmailDTO dto, [FromQuery] int? smtpSettingId)
        {
            _logger.LogInformation("Received smtpSettingId: {Id}", smtpSettingId);
            try
            {
                var result = await _emailService.SendEmailAsync(dto, smtpSettingId);

                return Ok(result);
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending email");
                return StatusCode(500, "An error occurred while  the Email sent.");
            }
        }
    }
}
