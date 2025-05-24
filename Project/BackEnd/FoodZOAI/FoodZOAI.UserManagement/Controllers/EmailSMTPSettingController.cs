using FoodZOAI.UserManagement.Configuration.Contracts;
using FoodZOAI.UserManagement.Configuration.Mappers;
using FoodZOAI.UserManagement.Contracts;
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodZOAI.UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailSMTPSettingController : ControllerBase
    {
        private readonly IEmailSettingRepository _emailSettingRepository;
        private readonly IEmailSettingMapper _emailSettingMapper;

        public EmailSMTPSettingController(IEmailSettingRepository emailSettingRepository,

           IEmailSettingMapper emailSettingMapper)
        {
            _emailSettingRepository = emailSettingRepository;
            _emailSettingMapper = emailSettingMapper;
        }
       
        [HttpGet("GetEmailSMTPSettings")]
        public async Task<IActionResult> GetEmailSMTPSettings()
        {
            try
            {
                var settings = await _emailSettingRepository.GetAllAsync();
                var result = _emailSettingMapper.MapList(settings.ToList());
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while retrieving the email settings.");
            }
        }

        [HttpGet("GetEmailSMTPSetting/{id}")]
        public async Task<IActionResult> GetEmailSMTPSettingById(int id)
        {
            try
            {
                var setting = await _emailSettingRepository.GetByIdAsync(id);
                if (setting == null)
                    return NotFound("Email setting not found.");

                var result = _emailSettingMapper.Map(setting);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while retrieving the email setting.");
            }
        }


        [HttpDelete("DeleteEmailSMTPSetting/{id}")]
        public async Task<IActionResult> DeleteEmailSMTPSetting(int id)
        {
            try
            {
                var exists = await _emailSettingRepository.ExistsAsync(id);
                if (!exists)
                    return NotFound("Email setting not found.");

                await _emailSettingRepository.DeleteAsync(id);
                return Ok("Email setting deleted successfully.");
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while deleting the email setting.");
            }
        }

        [HttpPost("AddEmailSMTPSetting")]
        public async Task<IActionResult> AddEmailSMTPSetting([FromBody] EmailSettingDTO templateDto)
        {
            try
            {
                if (templateDto == null)
                    return BadRequest("EmailSMTPSetting data is required.");

                var template = _emailSettingMapper.MapToDomain(templateDto);


                template.CreatedByUser = templateDto.CreatedByUser;

                await _emailSettingRepository.AddAsync(template);

                var resultDto = _emailSettingMapper.Map(template);
                return CreatedAtAction(nameof(GetEmailSMTPSettingById), new { id = template.Id }, resultDto);
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException != null ? ex.InnerException.Message : "";
                return StatusCode(500, $"An error occurred while adding the email template: {ex.Message} {innerMessage}");
            }
        }



        [HttpPut("UpdateEmailSMTPSetting/{id}")]
        public async Task<IActionResult> UpdateEmailSMTPSetting(int id, [FromBody] EmailSettingDTO updatedDto)
        {
            try
            {
                var existing = await _emailSettingRepository.GetByIdAsync(id);
                if (existing == null)
                    return NotFound("Email setting not found.");

                
                existing.Host = updatedDto.Host;
                existing.UserName = updatedDto.UserName;
                existing.Password = updatedDto.Password;
                existing.IsEnableSsl = updatedDto.IsEnableSsl;
                existing.IsDefault = updatedDto.IsDefault;
                existing.IsActive = updatedDto.IsActive;
                existing.ModifiedByUser = updatedDto.ModifiedByUser;

                await _emailSettingRepository.UpdateAsync(existing);

                var result = _emailSettingMapper.Map(existing);
                return Ok(result);
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException != null ? ex.InnerException.Message : "";
                return StatusCode(500, $"An error occurred while updating the email setting: {ex.Message} {innerMessage}");
            }
        }

















    }
}
