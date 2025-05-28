using FoodZOAI.UserManagement.DTOs;

namespace FoodZOAI.UserManagement.Services.Contracts
{
    public interface IHalfYearlyReminderService
    {
        Task<List<HalfYearlyReminderDTO>> GetAllAsync();
        Task<HalfYearlyReminderDTO?> GetByIdAsync(int id);
        Task<HalfYearlyReminderDTO> CreateAsync(HalfYearlyReminderDTO dto);
        Task<HalfYearlyReminderDTO> UpdateAsync(HalfYearlyReminderDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
