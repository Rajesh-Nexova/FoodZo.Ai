using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodZOAI.UserManagement.Models;
using FoodZOAI.UserManagement.Repositories;
using FoodZOAI.UserManagement.Configuration.Contracts;

namespace FoodZOAI.UserManagement.Services
{
    public interface IReminderNotificationService
    {
        Task<IEnumerable<ReminderNotification>> GetAllActiveAsync();
        Task<ReminderNotification?> GetByIdAsync(int id);
        Task<ReminderNotification> CreateAsync(ReminderNotification notification);
        Task<ReminderNotification?> UpdateAsync(int id, ReminderNotification notification);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<ReminderNotification>> GetByReminderIdAsync(int reminderId);
        Task<IEnumerable<ReminderNotification>> GetByUserIdAsync(int userId);
    }

    public class ReminderNotificationService : IReminderNotificationService
    {
        private readonly IReminderNotificationRepository _repository;

        public ReminderNotificationService(IReminderNotificationRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ReminderNotification>> GetAllActiveAsync()
        {
            var notifications = await _repository.GetAllAsync();
            return notifications.Where(n => n.IsActive);
        }

        public async Task<ReminderNotification?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<ReminderNotification> CreateAsync(ReminderNotification notification)
        {
            await _repository.AddAsync(notification); // no return here
            return notification;
        }

        public async Task UpdateAsync(int id, ReminderNotification notification)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return;

            existing.ReminderId = notification.ReminderId;
            existing.IsActive = notification.IsActive;
            existing.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(existing);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }


        public async Task<IEnumerable<ReminderNotification>> GetByReminderIdAsync(int reminderId)
        {
            var all = await _repository.GetAllAsync();
            return all.Where(n => n.ReminderId == reminderId);
        }

        public async Task<IEnumerable<ReminderNotification>> GetByUserIdAsync(int userId)
        {
            var all = await _repository.GetAllAsync();
           return all.Where(n => n.Reminder.UserId == userId);

        }

        Task<bool> IReminderNotificationService.DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<ReminderNotification?> IReminderNotificationService.UpdateAsync(int id, ReminderNotification notification)
        {
            throw new NotImplementedException();
        }
    }
}
