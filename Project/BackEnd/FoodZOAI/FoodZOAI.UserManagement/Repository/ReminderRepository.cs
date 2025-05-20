using FoodZOAI.UserManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodZOAI.UserManagement.Repositories
{
    public class ReminderRepository : IReminderRepository
    {
        private readonly FoodZoaiContext _context;

        public ReminderRepository(FoodZoaiContext context)
        {
            _context = context;
        }

        public async Task<Reminder> GetByIdAsync(int id)
        {
            return await _context.Reminders.FindAsync(id);
        }

        public async Task<IEnumerable<Reminder>> GetAllAsync()
        {
            return await _context.Reminders.ToListAsync();
        }

        public async Task AddAsync(Reminder entity)
        {
            await _context.Reminders.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Reminder entity)
        {
            _context.Reminders.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Reminders.FindAsync(id);
            if (entity != null)
            {
                _context.Reminders.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
