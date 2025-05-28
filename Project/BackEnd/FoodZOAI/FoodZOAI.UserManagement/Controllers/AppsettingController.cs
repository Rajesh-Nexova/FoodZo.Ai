using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Services.Contract;
using Microsoft.AspNetCore.Mvc;

namespace FoodZOAI.UserManagement.Controllers
{
    /// <summary>
    /// Santhosh Write TryCatch 
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AppsettingController:ControllerBase
    {
        private readonly IAppsettingsService _appsettingsService;
		private readonly ILogger<AppsettingController> _logger;

		public AppsettingController(IAppsettingsService appsettingsService,
			ILogger<AppsettingController> logger)
        {
           _appsettingsService = appsettingsService;
            _logger = logger;
        }

		public AppsettingController(IAppsettingsService appsettingsService)
		{
			_appsettingsService = appsettingsService;
		}

		[HttpGet("GetAllActiveSetting")] 
        public async Task<ActionResult<List<AppsettingDTO>>> GetAllAsync()
        {
            var list = await _appsettingsService.GetAllAppsettingsAsync();
            var activeOnly = list.Where(x => x.IsActive).ToList();
            return Ok(activeOnly);
        }

        [HttpGet("GetAllAppSettings")]
        public async Task<ActionResult<List<AppsettingDTO>>> GetAllAppSettingsAsync()
        {
            // returns all settings including inactive
            var list = await _appsettingsService.GetAllAppsettingsAsync();
            return Ok(list);
        }



        [HttpGet("GetAppSetting/{id}")]
        public async Task<ActionResult<AppsettingDTO>> GetAsync(int id)
        {
            var setting = await _appsettingsService.GetAppsettingByIdAsync(id);
            return setting == null ? NotFound() : Ok(setting);
        }
        [HttpGet("GetAppSettingByKey")]
        public async Task<ActionResult<AppsettingDTO>> GetByKeyAsync([FromQuery] string key)
        {
            var setting = await _appsettingsService.GetAppsettingByKeyAsync(key);
            return setting == null ? NotFound() : Ok(setting);
        }

        [HttpPost("AddAppSetting")]
        public async Task<ActionResult<AppsettingDTO>> AddAsync([FromBody] AppsettingDTO dto)
        {
            
			var added = await _appsettingsService.AddAppSettingAsync(dto);
            return CreatedAtAction(nameof(GetAsync), new { id = added.Id },
                added);
        }

        [HttpPut("UpdateAppSetting/{id}")]
        public async Task<ActionResult<AppsettingDTO>> UpdateAsync(int id, [FromBody] AppsettingDTO dto)
        {
           
            var updated = await _appsettingsService.UpdateAppSettingAsync(dto);
            return updated == null ? NotFound() : Ok(updated);
        }

        [HttpDelete("DeleteAppSetting/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _appsettingsService.DeleteAppSettingAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}
