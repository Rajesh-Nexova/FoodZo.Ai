using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodZOAI.UserManagement.Services.Contracts
{
    public interface IQuarterlyReminderService
    {
        Task<List<QuarterlyReminderDTO>> GetAllAsync();
        Task<QuarterlyReminderDTO?> GetByIdAsync(int id);
        Task<QuarterlyReminderDTO> CreateAsync(QuarterlyReminderDTO dto);
        Task<QuarterlyReminderDTO> UpdateAsync(QuarterlyReminderDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
