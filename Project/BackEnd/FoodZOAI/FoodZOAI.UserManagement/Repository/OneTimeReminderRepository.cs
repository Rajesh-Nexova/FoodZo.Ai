using FoodZOAI.UserManagement.Models;
using FoodZOAI.UserManagement.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodZOAI.UserManagement.Repositories.Implementations
{
    public class OneTimeReminderRepository : IOneTimeReminderRepository
    {
        private readonly FoodZoaiContext _context;

        public OneTimeReminderRepository(FoodZoaiContext context)
        {
            _context = context;
        }

        public async Task<List<OneTimeReminder>> GetAllAsync() => await _context.OneTimeReminders.ToListAsync();

        public async Task<OneTimeReminder?> GetByIdAsync(int id) => await _context.OneTimeReminders.FindAsync(id);

        public async Task AddAsync(OneTimeReminder reminder)
        {
            _context.OneTimeReminders.Add(reminder);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(OneTimeReminder reminder)
        {
            _context.OneTimeReminders.Update(reminder);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var reminder = await _context.OneTimeReminders.FindAsync(id);
            if (reminder != null)
            {
                _context.OneTimeReminders.Remove(reminder);
                await _context.SaveChangesAsync();
            }
        }
    }
}