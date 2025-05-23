using FoodZOAI.UserManagement.Contracts;
using FoodZOAI.UserManagement.Mappers.Interfaces;
using FoodZOAI.UserManagement.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FoodZOAI.UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HalfYearlyReminderController : ControllerBase
    {
        private readonly IHalfYearlyReminderRepository _repository;
        private readonly IHalfYearlyReminderMapper _mapper;

        public HalfYearlyReminderController(
            IHalfYearlyReminderRepository repository,
            IHalfYearlyReminderMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHalfYearlyReminderById(int id)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);
                if (entity == null)
                    return NotFound($"HalfYearlyReminder with id {id} not found.");

                var dto = _mapper.Map(entity);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server error: {ex.Message}");
            }
        }

    }
}
