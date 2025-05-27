using FoodZOAI.UserManagement.Configuration.Contracts;
using FoodZOAI.UserManagement.Configuration.Mappers;
using FoodZOAI.UserManagement.Contracts;
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;
using FoodZOAI.UserManagement.Repository;
using FoodZOAI.UserManagement.Services.Contract;
using Microsoft.AspNetCore.Mvc;

namespace FoodZOAI.UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailTemplateController : ControllerBase
    {

        private readonly IEmailTemplateService _emailTemplateService;
        private readonly ILogger<EmailSMTPSettingController> _logger;
        public EmailTemplateController(IEmailTemplateService emailTemplateService,

           ILogger<EmailSMTPSettingController> logger)
        {
            _emailTemplateService = emailTemplateService;
            _logger = logger;
        }

        [HttpGet("GetAllEmailTemplate")]
        public async Task<ActionResult<List<EmailTemplateDTO>>> GetAllAsync()
        {
            try
            {
                var settings = await _emailTemplateService.GetAllEmailTemplateAsync();
                return Ok(settings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all EmailTemplate.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetEmailTemplate/{id}")]
        public async Task<ActionResult<EmailTemplateDTO>> GetByIdAsync(int id)
        {
            try
            {
                var setting = await _emailTemplateService.GetEmailTemplateByIdAsync(id);
                return setting == null ? NotFound() : Ok(setting);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while fetching Email Template with id {id}.");
                return StatusCode(500, "Internal server error");
            }
        }

        



        [HttpPost("AddAppSetting")]
        public async Task<ActionResult<EmailTemplateDTO>> AddAsync([FromBody] EmailTemplateDTO dto)
        {

            var added = await _emailTemplateService.AddEmailTemplateAsync(dto);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = added.Id },
                added);
        }


        [HttpPut("UpdateEmailTemplate/{id}")]
        public async Task<ActionResult<EmailTemplateDTO>> UpdateAsync(int id, [FromBody] EmailTemplateDTO dto)
        {
            try
            {
                var updated = await _emailTemplateService.UpdateEmailTemplateAsync(id, dto);
                return updated == null ? NotFound() : Ok(updated);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while updating Email Template with id {id}.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("DeleteEmailTemplate/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var result = await _emailTemplateService.DeleteEmailTemplateAsync(id);
                return result ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while deleting Email SMTP setting with id {id}.");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
