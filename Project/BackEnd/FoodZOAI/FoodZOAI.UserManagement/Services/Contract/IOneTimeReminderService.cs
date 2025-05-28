using System.Collections.Generic;
using System.Threading.Tasks;
using FoodZOAI.UserManagement.DTOs;

namespace FoodZOAI.UserManagement.Services.Interfaces
{
    public interface IOneTimeReminderService
    {
        Task<List<OneTimeReminderDTO>> GetAllAsync();
        Task<OneTimeReminderDTO> GetByIdAsync(int id);
        Task<OneTimeReminderDTO> CreateAsync(OneTimeReminderDTO dto);
        Task<OneTimeReminderDTO> UpdateAsync(OneTimeReminderDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
