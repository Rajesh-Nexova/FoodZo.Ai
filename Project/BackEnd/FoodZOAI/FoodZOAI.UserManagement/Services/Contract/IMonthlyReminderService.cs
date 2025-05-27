using System.Collections.Generic;
using System.Threading.Tasks;
using FoodZOAI.UserManagement.DTOs;

namespace FoodZOAI.UserManagement.Services.Contracts
{
    public interface IMonthlyReminderService
    {
        Task<List<MonthlyReminderDTO>> GetAllAsync();
        Task<MonthlyReminderDTO?> GetByIdAsync(int id);
        Task<MonthlyReminderDTO> CreateAsync(MonthlyReminderDTO dto);
        Task<MonthlyReminderDTO> UpdateAsync(MonthlyReminderDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
