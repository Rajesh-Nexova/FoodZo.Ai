using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement.Contracts
{
    public interface IOneTimeReminderRepository
    {
        Task<List<OneTimeReminder>> GetAllAsync();
    }
}
