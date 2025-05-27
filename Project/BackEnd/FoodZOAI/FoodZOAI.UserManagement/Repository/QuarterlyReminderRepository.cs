using FoodZOAI.UserManagement.Models;
using FoodZOAI.UserManagement.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodZOAI.UserManagement.Repositories
{
    public class QuarterlyReminderRepository : IQuarterlyReminderRepository
    {
        private readonly FoodZoaiContext _context;

        public QuarterlyReminderRepository(FoodZoaiContext context)
        {
            _context = context;
        }

        public async Task<List<QuarterlyReminder>> GetAllAsync()
        {
            return await _context.QuarterlyReminders.ToListAsync();
        }

        public async Task<QuarterlyReminder?> GetByIdAsync(int id)
        {
            return await _context.QuarterlyReminders.FindAsync(id);
        }

        public async Task<QuarterlyReminder> AddAsync(QuarterlyReminder entity)
        {
            _context.QuarterlyReminders.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<QuarterlyReminder> UpdateAsync(QuarterlyReminder entity)
        {
            _context.QuarterlyReminders.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.QuarterlyReminders.FindAsync(id);
            if (entity == null) return false;
            _context.QuarterlyReminders.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
