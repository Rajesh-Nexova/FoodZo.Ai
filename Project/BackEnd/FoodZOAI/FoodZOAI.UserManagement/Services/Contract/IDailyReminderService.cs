using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodZOAI.UserManagement.Services.Contracts
{
    public interface IDailyReminderService
    {
        Task<List<DailyReminderDTO>> GetAllAsync();
        Task<DailyReminderDTO?> GetByIdAsync(int id);
        Task<DailyReminderDTO> CreateAsync(DailyReminderDTO dto);
        Task<DailyReminderDTO> UpdateAsync(DailyReminderDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
