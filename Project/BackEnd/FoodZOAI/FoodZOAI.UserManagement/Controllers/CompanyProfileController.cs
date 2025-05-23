using FoodZOAI.UserManagement.Configuration.Contracts;
using FoodZOAI.UserManagement.Contracts;
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FoodZOAI.UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyProfileController : ControllerBase
    {
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IOrganizationMapper _organizationMapper;
        public CompanyProfileController(IOrganizationRepository organizationRepository, IOrganizationMapper organizationMapper)
        {
            _organizationRepository = organizationRepository;
            _organizationMapper = organizationMapper;
        }

        [HttpGet("GetOrganisation{id}")]

        public async Task<IActionResult> GetByIDAsync(int id)
        {
            try
            {
                var organisation = await _organizationRepository.GetByIdAsync(id);
                if (organisation == null)
                    return NotFound("Organization ID Not Found");

                var result = _organizationMapper.MapToDTO(organisation);

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "An Error Occured while retrieving the data");
            }
        }

        [HttpPut("UpdateOrganisation{id}")]
        public async Task<IActionResult> UpdateOrganisation(int id, [FromBody] OrganizationDTO updatedto)
        {
            try
            {
                var existing = await _organizationRepository.GetByIdAsync(id);
                if (existing == null)
                    return NotFound(" ID Not Found ");

                existing.Id = updatedto.Id;
                existing.Name = updatedto.Name;
                existing.Slug = updatedto.Slug;
                existing.Description = updatedto.Description;

                existing.Website = updatedto.Website;
                existing.LogoUrl = updatedto.LogoUrl;
                existing.SubscriptionPlan = updatedto.SubscriptionPlan;
                existing.MaxUsers = updatedto.MaxUsers;

                existing.Status = updatedto.Status;
                existing.CreatedAt = updatedto.CreatedAt;
                existing.UpdatedAt = updatedto.UpdatedAt;
                existing.DeletedAt = updatedto.DeletedAt;

                await _organizationRepository.UpdateAsync(existing);

                var result = _organizationMapper.MapToDTO(existing);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $" An Error Occured While Updating the Data{ex.Message}{ex.InnerException}");
            }
        }

    }
}
