using FoodZOAI.UserManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodZOAI.UserManagement.Repositories
{
    public class HalfYearlyReminderRepository : IHalfYearlyReminderRepository
    {
        private readonly FoodZoaiContext _context;

        public HalfYearlyReminderRepository(FoodZoaiContext context)
        {
            _context = context;
        }

        public async Task<HalfYearlyReminder> GetByIdAsync(int id)
        {
            return await _context.HalfYearlyReminders.FindAsync(id);
        }

        public async Task<IEnumerable<HalfYearlyReminder>> GetAllAsync()
        {
            return await _context.HalfYearlyReminders.ToListAsync();
        }

        public async Task AddAsync(HalfYearlyReminder entity)
        {
            await _context.HalfYearlyReminders.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(HalfYearlyReminder entity)
        {
            _context.HalfYearlyReminders.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.HalfYearlyReminders.FindAsync(id);
            if (entity != null)
            {
                _context.HalfYearlyReminders.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
