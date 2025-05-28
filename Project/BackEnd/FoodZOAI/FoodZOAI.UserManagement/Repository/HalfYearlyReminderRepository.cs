using FoodZOAI.UserManagement.Models;
using FoodZOAI.UserManagement.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace FoodZOAI.UserManagement.Repositories
{
    public class HalfYearlyReminderRepository : IHalfYearlyReminderRepository
    {
        private readonly FoodZoaiContext _context;

        public HalfYearlyReminderRepository(FoodZoaiContext context)
        {
            _context = context;
        }

        public async Task<List<HalfYearlyReminder>> GetAllAsync()
        {
            return await _context.HalfYearlyReminders.ToListAsync();
        }

        public async Task<HalfYearlyReminder?> GetByIdAsync(int id)
        {
            return await _context.HalfYearlyReminders.FindAsync(id);
        }

        public async Task<HalfYearlyReminder> AddAsync(HalfYearlyReminder entity)
        {
            _context.HalfYearlyReminders.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<HalfYearlyReminder> UpdateAsync(HalfYearlyReminder entity)
        {
            _context.HalfYearlyReminders.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.HalfYearlyReminders.FindAsync(id);
            if (entity == null) return false;
            _context.HalfYearlyReminders.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
