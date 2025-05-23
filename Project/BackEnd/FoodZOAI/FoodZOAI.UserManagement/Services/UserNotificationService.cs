using System.Collections.Generic;
using System.Threading.Tasks;
using FoodZOAI.UserManagement.Contracts;
using FoodZOAI.UserManagement.Models;
using FoodZOAI.UserManagement.Repositories;

namespace FoodZOAI.UserManagement.Services
{
    public interface IUserNotificationService
    {
        Task<IEnumerable<UserNotification>> GetAllAsync();
        Task<UserNotification?> GetByIdAsync(int id);
        Task<UserNotification> CreateAsync(UserNotification notification);
        Task<UserNotification?> UpdateAsync(int id, UserNotification notification);
        Task<bool> DeleteAsync(int id);
    }

    public class UserNotificationService : IUserNotificationService
    {
        private readonly IUserNotificationRepository _repository;

        public UserNotificationService(IUserNotificationRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<UserNotification>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<UserNotification?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<UserNotification> CreateAsync(UserNotification notification)
        {
            return await _repository.AddAsync(notification);
        }

        public async Task<UserNotification?> UpdateAsync(int id, UserNotification notification)
        {
            var existingNotification = await _repository.GetByIdAsync(id);
            if (existingNotification == null)
                return null;

            // Update fields
            existingNotification.Message = notification.Message;
            existingNotification.IsRead = notification.IsRead;
            existingNotification.NotificationsType = notification.NotificationsType;
            existingNotification.UserId = notification.UserId;

            return await _repository.UpdateAsync(existingNotification);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var notification = await _repository.GetByIdAsync(id);
            if (notification == null)
                return false;

            await _repository.DeleteAsync(notification);
            return true;
        }
    }
}
