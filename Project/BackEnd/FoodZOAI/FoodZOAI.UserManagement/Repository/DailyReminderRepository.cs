using FoodZOAI.UserManagement.Models;
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

        public async Task<DailyReminder> GetByIdAsync(int id)
        {
            return await _context.DailyReminders
                                 .Include(d => d.Reminder) // optional eager loading
                                 .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<IEnumerable<DailyReminder>> GetAllAsync()
        {
            return await _context.DailyReminders
                                 .Include(d => d.Reminder)
                                 .ToListAsync();
        }

        public async Task AddAsync(DailyReminder entity)
        {
            await _context.DailyReminders.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(DailyReminder entity)
        {
            _context.DailyReminders.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.DailyReminders.FindAsync(id);
            if (entity != null)
            {
                _context.DailyReminders.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
