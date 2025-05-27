using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Services.Contract;
using Microsoft.AspNetCore.Mvc;

namespace FoodZOAI.UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyProfileController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;

        public CompanyProfileController(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        [HttpGet("GetEmailTemplate/{id}")]
        public async Task<ActionResult<OrganizationDTO>> GetByIdAsync(int id)
        {
            try
            {
                var setting = await _organizationService.GetOrganizationByIdAsync(id);
                return setting == null ? NotFound() : Ok(setting);
            }
            catch (Exception ex)
            {
              
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpPut("UpdateEmailSMTPSetting/{id}")]
        public async Task<ActionResult<OrganizationDTO>> UpdateAsync(int id, [FromBody] OrganizationDTO dto)
        {
            try
            {
                var updated = await _organizationService.UpdateOrganizationAsync(id, dto);
                return updated == null ? NotFound() : Ok(updated);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, "Internal server error");
            }
        }


    }
}
