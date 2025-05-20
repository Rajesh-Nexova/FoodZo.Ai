using FoodZOAI.UserManagement.Contracts;
using FoodZOAI.UserManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodZOAI.UserManagement.Repositories
{
    public class UserNotificationRepository : IUserNotificationRepository
    {
        private readonly FoodZoaiContext _context;

        public UserNotificationRepository(FoodZoaiContext context)
        {
            _context = context;
        }

        public async Task<List<UserNotification>> GetAllAsync()
        {
            return await _context.UserNotifications
                .Where(n => n.IsActive)
                .ToListAsync();
        }

        public async Task<UserNotification?> GetByIdAsync(int id)
        {
            return await _context.UserNotifications
                .FirstOrDefaultAsync(n => n.Id == id && n.IsActive);
        }

        public async Task<List<UserNotification>> GetByUserIdAsync(int userId)
        {
            return await _context.UserNotifications
                .Where(n => n.UserId == userId && n.IsActive)
                .ToListAsync();
        }

        public async Task<UserNotification> AddAsync(UserNotification notification)
        {
            notification.CreatedAt = DateTime.UtcNow;
            notification.IsActive = true;

            _context.UserNotifications.Add(notification);
            await _context.SaveChangesAsync();
            return notification;
        }

        public async Task<UserNotification?> UpdateAsync(UserNotification notification)
        {
            var existing = await _context.UserNotifications.FindAsync(notification.Id);
            if (existing == null || !existing.IsActive)
                return null;

            existing.Message = notification.Message;
            existing.IsRead = notification.IsRead;
            existing.NotificationsType = notification.NotificationsType;
          

            _context.UserNotifications.Update(existing);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.UserNotifications.FindAsync(id);
            if (existing == null || !existing.IsActive)
                return false;

            // Soft delete
            existing.IsActive = false;
            existing.DeletedAt = DateTime.UtcNow;

            _context.UserNotifications.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public Task DeleteAsync(UserNotification notification)
        {
            throw new NotImplementedException();
        }
    }
}
