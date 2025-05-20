using FoodZOAI.UserManagement.Configuration.Contracts;
using FoodZOAI.UserManagement.Configuration.Mappers;
using FoodZOAI.UserManagement.Contracts;
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;
using FoodZOAI.UserManagement.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FoodZOAI.UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailTemplateController : ControllerBase
    {
        private readonly IEmailTemplateRepository _emailTemplateRepository;
        private readonly IEmailTemplateMapper _emailTemplateMapper;

        public EmailTemplateController(IEmailTemplateRepository emailTemplateRepository,

           IEmailTemplateMapper emailTemplateMapper)
        {
            _emailTemplateRepository = emailTemplateRepository;
            _emailTemplateMapper = emailTemplateMapper;
        }

        [HttpGet("GetEmailTemplates")]
        public async Task<IActionResult> GetEmailTemplates()
        {
            try
            {
                var settings = await _emailTemplateRepository.GetAllAsync();
                var result = _emailTemplateMapper.MapList(settings.ToList());
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while retrieving the email settings.");
            }
        }

        [HttpGet("GetEmailTemplate/{id}")]
        public async Task<IActionResult> GetEmailTemplateById(int id)
        {
            try
            {
                var setting = await _emailTemplateRepository.GetByIdAsync(id);
                if (setting == null)
                    return NotFound("Email setting not found.");

                var result = _emailTemplateMapper.MapToDTO(setting);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while retrieving the email Template.");
            }
        }


        [HttpDelete("DeleteEmailTemplate/{id}")]
        public async Task<IActionResult> DeleteEmailTemplate(int id)
        {
            try
            {
                var exists = await _emailTemplateRepository.ExistsAsync(id);
                if (!exists)
                    return NotFound("Email Template not found.");

                await _emailTemplateRepository.DeleteAsync(id);
                return Ok("Email Template deleted successfully.");
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while deleting the email Template.");
            }
        }



        [HttpPut("UpdateEmailTemplate/{id}")]
        public async Task<IActionResult> UpdateEmailTemplate(int id, [FromBody] EmailTemplateDTO updatedDto)
        {
            try
            {
                var existing = await _emailTemplateRepository.GetByIdAsync(id);
                if (existing == null)
                    return NotFound("Email template not found.");

                // Update fields
                existing.Name = updatedDto.Name;
                existing.Subject = updatedDto.Subject;
                existing.Body = updatedDto.Body;
                existing.IsActive = updatedDto.IsActive;
                existing.ModifiedByUser = updatedDto.ModifiedByUser;

                await _emailTemplateRepository.UpdateAsync(existing);

                var result = _emailTemplateMapper.MapToDTO(existing);
                return Ok(result);
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException != null ? ex.InnerException.Message : "";
                return StatusCode(500, $"An error occurred while updating the email template: {ex.Message} {innerMessage}");
            }
        }

        [HttpPost("AddEmailTemplate")]
        public async Task<IActionResult> AddEmailTemplate([FromBody] EmailTemplateDTO templateDto)
        {
            try
            {
                if (templateDto == null)
                    return BadRequest("Email template data is required.");

                var template = _emailTemplateMapper.MapToDomain(templateDto);

                
                template.CreatedByUser = templateDto.CreatedByUser;

                await _emailTemplateRepository.AddAsync(template);

                var resultDto = _emailTemplateMapper.MapToDTO(template);
                return CreatedAtAction(nameof(GetEmailTemplateById), new { id = template.Id }, resultDto);
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException != null ? ex.InnerException.Message : "";
                return StatusCode(500, $"An error occurred while adding the email template: {ex.Message} {innerMessage}");
            }
        }


    }
}
