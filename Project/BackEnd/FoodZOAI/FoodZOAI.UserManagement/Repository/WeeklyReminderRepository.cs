using FoodZOAI.UserManagement.Models;
using FoodZOAI.UserManagement.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodZOAI.UserManagement.Repositories
{
    public class WeeklyReminderRepository : IWeeklyReminderRepository
    {
        private readonly FoodZoaiContext _context;

        public WeeklyReminderRepository(FoodZoaiContext context)
        {
            _context = context;
        }

        public async Task<List<WeeklyReminder>> GetAllAsync()
        {
            return await _context.WeeklyReminders
                .AsNoTracking()
                .Where(wr => wr.DeletedAt == null) // exclude soft deleted
                .ToListAsync();
        }

        public async Task<WeeklyReminder?> GetByIdAsync(int id)
        {
            return await _context.WeeklyReminders
                .FirstOrDefaultAsync(wr => wr.Id == id && wr.DeletedAt == null);
        }

        public async Task<WeeklyReminder> AddAsync(WeeklyReminder entity)
        {
            await _context.WeeklyReminders.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<WeeklyReminder> UpdateAsync(WeeklyReminder entity)
        {
            _context.WeeklyReminders.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.WeeklyReminders.FindAsync(id);
            if (entity == null) return false;

            // Soft delete: mark DeletedAt and IsActive false
            entity.DeletedAt = DateTime.UtcNow;
            entity.IsActive = false;

            _context.WeeklyReminders.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
