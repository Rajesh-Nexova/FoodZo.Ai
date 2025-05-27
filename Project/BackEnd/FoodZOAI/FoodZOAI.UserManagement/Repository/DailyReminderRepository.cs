using FoodZOAI.UserManagement.Models;
using FoodZOAI.UserManagement.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodZOAI.UserManagement.Repositories
{
    public class DailyReminderRepository : IDailyReminderRepository
    {
        private readonly FoodZoaiContext _context;

        public DailyReminderRepository(FoodZoaiContext context)
        {
            _context = context;
        }

        public async Task<List<DailyReminder>> GetAllAsync()
        {
            return await _context.DailyReminders.ToListAsync();
        }

        public async Task<DailyReminder?> GetByIdAsync(int id)
        {
            return await _context.DailyReminders.FindAsync(id);
        }

        public async Task<DailyReminder> AddAsync(DailyReminder reminder)
        {
            _context.DailyReminders.Add(reminder);
            await _context.SaveChangesAsync();
            return reminder;
        }

        public async Task<DailyReminder> UpdateAsync(DailyReminder reminder)
        {
            _context.DailyReminders.Update(reminder);
            await _context.SaveChangesAsync();
            return reminder;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.DailyReminders.FindAsync(id);
            if (entity == null) return false;

            _context.DailyReminders.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
