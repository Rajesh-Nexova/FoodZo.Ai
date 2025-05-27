// MonthlyReminderRepository.cs
using FoodZOAI.UserManagement.Models;
using FoodZOAI.UserManagement.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace FoodZOAI.UserManagement.Repositories
{
    public class MonthlyReminderRepository : IMonthlyReminderRepository
    {
        private readonly FoodZoaiContext _context;

        public MonthlyReminderRepository(FoodZoaiContext context)
        {
            _context = context;
        }

        public async Task<List<MonthlyReminder>> GetAllAsync() =>
            await _context.MonthlyReminders.ToListAsync();

        public async Task<MonthlyReminder?> GetByIdAsync(int id) =>
            await _context.MonthlyReminders.FindAsync(id);

        public async Task<MonthlyReminder> AddAsync(MonthlyReminder reminder)
        {
            _context.MonthlyReminders.Add(reminder);
            await _context.SaveChangesAsync();
            return reminder;
        }

        public async Task<MonthlyReminder> UpdateAsync(MonthlyReminder reminder)
        {
            _context.MonthlyReminders.Update(reminder);
            await _context.SaveChangesAsync();
            return reminder;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var reminder = await _context.MonthlyReminders.FindAsync(id);
            if (reminder == null) return false;

            _context.MonthlyReminders.Remove(reminder);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
