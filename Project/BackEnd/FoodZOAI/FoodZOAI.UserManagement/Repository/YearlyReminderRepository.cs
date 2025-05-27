using FoodZOAI.UserManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodZOAI.UserManagement.Repository
{
    public class YearlyReminderRepository : IYearlyReminderRepository
    {
        private readonly FoodZoaiContext _context;

        public YearlyReminderRepository(FoodZoaiContext context)
        {
            _context = context;
        }

        public async Task<List<YearlyReminder>> GetAllAsync()
        {
            return await _context.YearlyReminders.ToListAsync();
        }

        public async Task<YearlyReminder?> GetByIdAsync(int id)
        {
            return await _context.YearlyReminders.FindAsync(id);
        }

        public async Task<YearlyReminder> CreateAsync(YearlyReminder reminder)
        {
            _context.YearlyReminders.Add(reminder);
            await _context.SaveChangesAsync();
            return reminder;
        }

        public async Task<YearlyReminder> UpdateAsync(YearlyReminder reminder)
        {
            _context.YearlyReminders.Update(reminder);
            await _context.SaveChangesAsync();
            return reminder;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var reminder = await _context.YearlyReminders.FindAsync(id);
            if (reminder == null) return false;

            _context.YearlyReminders.Remove(reminder);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
