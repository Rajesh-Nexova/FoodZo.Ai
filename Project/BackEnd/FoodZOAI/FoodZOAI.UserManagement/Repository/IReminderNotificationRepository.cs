using FoodZOAI.UserManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodZOAI.UserManagement.Repositories
{
    public interface IReminderNotificationRepository
    {
        Task<ReminderNotification> GetByIdAsync(int id);
        Task<IEnumerable<ReminderNotification>> GetAllAsync();
        Task AddAsync(ReminderNotification entity);
        Task UpdateAsync(ReminderNotification entity);
        Task DeleteAsync(int id);
    }
}
