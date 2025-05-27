using FoodZOAI.UserManagement.Contracts;
using FoodZOAI.UserManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace FoodZOAI.UserManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;
        private readonly ILogger<RoleController> _logger;

        public RoleController(IRoleRepository roleRepository, ILogger<RoleController> logger)
        {
            _roleRepository = roleRepository;
            _logger = logger;
        }

        // POST: api/role
        [HttpPost]
        public async Task<IActionResult> AddRole([FromBody] Role role)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state when adding a new role.");
                return BadRequest(ModelState);
            }

            var createdRole = await _roleRepository.AddRoleAsync(role);
            _logger.LogInformation("Role created successfully with ID {Id}", createdRole.Id);
            return CreatedAtAction(nameof(GetRoleById), new { id = createdRole.Id }, createdRole);
        }

        // PUT: api/role
        [HttpPut]
        public async Task<IActionResult> UpdateRole([FromBody] Role role)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state when updating role with ID {Id}", role.Id);
                return BadRequest(ModelState);
            }

            var existingRole = await _roleRepository.GetByIdAsync(role.Id);
            if (existingRole == null)
            {
                _logger.LogWarning("Role with ID {Id} not found for update", role.Id);
                return NotFound($"Role with ID {role.Id} not found.");
            }

            var updatedRole = await _roleRepository.UpdateAsync(role);
            _logger.LogInformation("Role updated successfully with ID {Id}", role.Id);
            return Ok(updatedRole);
        }

        // GET: api/role/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            if (role == null)
            {
                _logger.LogWarning("Role with ID {Id} not found.", id);
                return NotFound($"Role with ID {id} not found.");
            }

            _logger.LogInformation("Role retrieved with ID {Id}", id);
            return Ok(role);
        }

        // GET: api/role
        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _roleRepository.GetAllAsync();
            _logger.LogInformation("Retrieved {Count} roles.", roles.Count());
            return Ok(roles);
        }

        // DELETE: api/role/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var deleted = await _roleRepository.DeleteAsync(id);
            if (!deleted)
            {
                _logger.LogWarning("Attempted to delete non-existing role with ID {Id}", id);
                return NotFound($"Role with ID {id} not found.");
            }

            _logger.LogInformation("Role deleted with ID {Id}", id);
            return NoContent();
        }
    }
}
