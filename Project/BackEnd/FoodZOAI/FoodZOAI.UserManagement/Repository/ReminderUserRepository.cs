using FoodZOAI.UserManagement.Contracts;
using FoodZOAI.UserManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodZOAI.UserManagement.Repositories
{
    public class ReminderUserRepository : IReminderUserRepository
    {
        private readonly FoodZoaiContext _context;

        public ReminderUserRepository(FoodZoaiContext context)
        {
            _context = context;
        }

        public async Task<List<ReminderUser>> GetAllAsync()
        {
            return await _context.ReminderUsers
                .Include(ru => ru.Reminder)  // Optional: eager load related Reminder
                .ToListAsync();
        }

        public async Task<ReminderUser?> GetByIdAsync(int id)
        {
            return await _context.ReminderUsers
                .Include(ru => ru.Reminder)
                .FirstOrDefaultAsync(ru => ru.Id == id);
        }

        public async Task<ReminderUser> AddAsync(ReminderUser reminderUser)
        {
            _context.ReminderUsers.Add(reminderUser);
            await _context.SaveChangesAsync();
            return reminderUser;
        }

        public async Task<ReminderUser?> UpdateAsync(ReminderUser reminderUser)
        {
            var existing = await _context.ReminderUsers.FindAsync(reminderUser.Id);
            if (existing == null) return null;

            existing.ReminderId = reminderUser.ReminderId;
            existing.UserId = reminderUser.UserId;
            existing.IsActive = reminderUser.IsActive;

            _context.ReminderUsers.Update(existing);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.ReminderUsers.FindAsync(id);
            if (existing == null) return false;

            // Soft delete (if you have IsActive flag)
            existing.IsActive = false;
            existing.DeletedAt = DateTime.UtcNow;
            _context.ReminderUsers.Update(existing);

            // Or hard delete:
            // _context.ReminderUsers.Remove(existing);

            await _context.SaveChangesAsync();
            return true;
        }

        public Task<ReminderUser?> GetByIdAsync(int reminderId, int id)
        {
            throw new NotImplementedException();
        }
    }
}
