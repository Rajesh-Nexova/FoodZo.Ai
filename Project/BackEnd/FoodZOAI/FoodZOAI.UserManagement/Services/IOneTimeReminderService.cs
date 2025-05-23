using FoodZOAI.UserManagement.DTOs;

namespace FoodZOAI.UserManagement.Contracts
{
    public interface IOneTimeReminderService
    {
        Task<List<OneTimeReminderDTO>> GetOneTimeRemindersAsync();
    }
}
