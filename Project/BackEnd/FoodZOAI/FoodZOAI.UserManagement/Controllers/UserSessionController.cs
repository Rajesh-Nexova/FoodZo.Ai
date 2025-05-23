
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Mappers.Interfaces;
using FoodZOAI.UserManagement.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FoodZOAI.UserManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserSessionController : ControllerBase
    {
        private readonly IUserSessionRepository _repository;
        private readonly IUserSessionMapper _mapper;

        public UserSessionController(IUserSessionRepository repository, IUserSessionMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var sessions = await _repository.GetAllAsync();
            return Ok(_mapper.MapList(sessions.ToList()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var session = await _repository.GetByIdAsync(id);
            if (session == null)
                return NotFound();

            return Ok(_mapper.MapToDTO(session));
        }
    }
}
