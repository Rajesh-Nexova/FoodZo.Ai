using System.Collections.Generic;
using System.Threading.Tasks;
using FoodZOAI.UserManagement.DTOs;

namespace FoodZOAI.UserManagement.Services.Contracts
{
    public interface IWeeklyReminderService
    {
        Task<List<WeeklyReminderDTO>> GetAllAsync();
        Task<WeeklyReminderDTO?> GetByIdAsync(int id);
        Task<WeeklyReminderDTO> CreateAsync(WeeklyReminderDTO dto);
        Task<WeeklyReminderDTO> UpdateAsync(WeeklyReminderDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
