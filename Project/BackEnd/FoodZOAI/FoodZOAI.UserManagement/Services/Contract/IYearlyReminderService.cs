using FoodZOAI.UserManagement.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodZOAI.UserManagement.Services
{
    public interface IYearlyReminderService
    {
        Task<List<YearlyReminderDTO>> GetAllAsync();
        Task<YearlyReminderDTO?> GetByIdAsync(int id);
        Task<YearlyReminderDTO> CreateAsync(YearlyReminderDTO dto);
        Task<YearlyReminderDTO> UpdateAsync(YearlyReminderDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
