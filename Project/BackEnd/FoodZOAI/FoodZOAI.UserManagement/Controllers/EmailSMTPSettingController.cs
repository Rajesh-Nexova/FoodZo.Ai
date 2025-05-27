using FoodZOAI.UserManagement.CustomMiddleWare;
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Services.Contract;
using Microsoft.AspNetCore.Mvc;

namespace FoodZOAI.UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class EmailSMTPSettingController : ControllerBase
    {
        private readonly IEmailSMTPSettingService _emailSMTPSettingService;
        private readonly ILogger<EmailSMTPSettingController> _logger;


        public EmailSMTPSettingController(IEmailSMTPSettingService emailSMTPSettingService, ILogger<EmailSMTPSettingController> logger)
        {
            _emailSMTPSettingService = emailSMTPSettingService;
            _logger = logger;


        }

        public EmailSMTPSettingController(IEmailSMTPSettingService emailSMTPSettingservice)
        {
            _emailSMTPSettingService = emailSMTPSettingservice;
        }


        [HttpGet("GetAllEmailSMTPSettings")]
        public async Task<ActionResult<List<EmailSettingDTO>>> GetAllAsync()
        {
            try
            {
                EmailSettingLogger.LogInfo("Fetching all Email SMTP settings");
                var settings = await _emailSMTPSettingService.GetAllEmailSMTPSettingAsync();
                return Ok(settings);
            }
            catch (Exception ex)
            {
                EmailSettingLogger.LogError("Error while fetching all Email SMTP settings.", ex);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetEmailSMTPSetting/{id}")]
        public async Task<ActionResult<EmailSettingDTO>> GetByIdAsync(int id)
        {
            try
            {
                EmailSettingLogger.LogInfo($"Fetching Email SMTP setting with id: {id}");
                var setting = await _emailSMTPSettingService.GetEmailSMTPSettingByIdAsync(id);
                return setting == null ? NotFound() : Ok(setting);
            }
            catch (Exception ex)
            {
                EmailSettingLogger.LogError($"Error while fetching Email SMTP setting with id {id}.", ex);

                _logger.LogError(ex, $"Error while fetching Email SMTP setting with id {id}.");
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpPost("AddEmailSMTPSetting")]
        public async Task<ActionResult<EmailSettingDTO>> AddAsync([FromBody] EmailSettingDTO dto)
        {
            try

            {
                EmailSettingLogger.LogInfo("Adding new Email SMTP setting", dto);

                var created = await _emailSMTPSettingService.AddEmailSMTPSettingAsync(dto);
                return CreatedAtAction(nameof(GetByIdAsync), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                EmailSettingLogger.LogError("Error while adding Email SMTP setting.", ex);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("UpdateEmailSMTPSetting/{id}")]
        public async Task<ActionResult<EmailSettingDTO>> UpdateAsync(int id, [FromBody] EmailSettingDTO dto)
        {
            try
            {
                EmailSettingLogger.LogInfo($"Updating Email SMTP setting with id {id}", dto);

                var updated = await _emailSMTPSettingService.UpdateEmailSMTPSettingAsync(id, dto);
                return updated == null ? NotFound() : Ok(updated);
            }
            catch (Exception ex)
            {
                EmailSettingLogger.LogError($"Error while updating Email SMTP setting with id {id}.", ex);
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpDelete("DeleteEmailSMTPSetting/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                EmailSettingLogger.LogInfo($"Deleting Email SMTP setting with id {id}");

                var result = await _emailSMTPSettingService.DeleteEmailSMTPSettingAsync(id);
                return result ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                EmailSettingLogger.LogError($"Error while deleting Email SMTP setting with id {id}.", ex);
                return StatusCode(500, "Internal server error");
            }
        }




    }
}

