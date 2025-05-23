using FoodZOAI.UserManagement.Models;
using FoodZOAI.UserManagement.Contracts;
using Microsoft.EntityFrameworkCore;

namespace FoodZOAI.UserManagement.Repositories
{
    public class OneTimeReminderRepository : IOneTimeReminderRepository
    {
        private readonly FoodZoaiContext _context;

        public OneTimeReminderRepository(FoodZoaiContext context)
        {
            _context = context;
        }

        public async Task<List<OneTimeReminder>> GetAllAsync()
        {
            return await _context.OneTimeReminders
                .Where(r => r.DeletedAt == null)
                .ToListAsync();
        }
    }
}
