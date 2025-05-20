using FoodZOAI.UserManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodZOAI.UserManagement.Contracts
{
    public interface IUserNotificationRepository
    {
        Task<List<UserNotification>> GetAllAsync();
        Task<UserNotification?> GetByIdAsync(int id);
        Task<List<UserNotification>> GetByUserIdAsync(int userId);
        Task<UserNotification> AddAsync(UserNotification notification);
        Task<UserNotification?> UpdateAsync(UserNotification notification);
        Task<bool> DeleteAsync(int id);
        Task DeleteAsync(UserNotification notification);
    }
}
