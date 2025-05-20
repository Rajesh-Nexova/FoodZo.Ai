using FoodZOAI.UserManagement.Configuration.Contracts;
using FoodZOAI.UserManagement.Contracts;
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace FoodZOAI.UserManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppsettingController:ControllerBase
    {
        private readonly IAppsettingRepository _appsettingRepository;
        private readonly IAppsetting _mapper;

        public AppsettingController(IAppsettingRepository appsettingRepository, IAppsetting mapper)
        {
            _appsettingRepository = appsettingRepository;
            _mapper = mapper;
        }

        [HttpGet("GetAllAppSetting")] 
        public async Task<IActionResult> GetAll()
        {
            var list = await _appsettingRepository.GetAllAsync();
            var activeOnly = list.Where(x => x.IsActive).ToList();
            return Ok(_mapper.MapList(activeOnly));
        }

        [HttpGet("GetAllAppSettings")]
        public async Task<IActionResult> GetAllAppSettings()
        {
            // returns all settings including inactive
            var list = await _appsettingRepository.GetAllAsync();
            return Ok(_mapper.MapList(list.ToList()));
        }



        [HttpGet("GetAppSetting/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var setting = await _appsettingRepository.GetByIdAsync(id);
            return setting == null ? NotFound() : Ok(_mapper.Map(setting));
        }
        [HttpGet("GetAppSettingByKey")]
        public async Task<IActionResult> GetByKey([FromQuery] string key)
        {
            var setting = await _appsettingRepository.GetByKeyAsync(key);
            return setting == null ? NotFound() : Ok(_mapper.Map(setting));
        }

        [HttpPost("AddAppSetting")]
        public async Task<IActionResult> Add([FromBody] AppsettingDTO dto)
        {
            var setting = new Appsetting
            {
                Name = dto.Name,
                Key = dto.Key,
                Value = dto.Value,
                CreatedByUser = dto.CreatedByUser,
                IsActive = dto.IsActive
            };
            var added = await _appsettingRepository.AddAsync(setting);
            return CreatedAtAction(nameof(Get), new { id = added.Id }, _mapper.Map(added));
        }

        [HttpPut("UpdateAppSetting/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AppsettingDTO dto)
        {
            var setting = new Appsetting
            {
                Name = dto.Name,
                Key = dto.Key,
                Value = dto.Value,
                ModifiedByUser = dto.ModifiedByUser,
                IsActive = dto.IsActive
            };
            var updated = await _appsettingRepository.UpdateAsync(id, setting);
            return updated == null ? NotFound() : Ok(_mapper.Map(updated));
        }

        [HttpDelete("DeleteAppSetting/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _appsettingRepository.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}
