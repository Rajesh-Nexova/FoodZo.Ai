using FoodZOAI.UserManagement.Contracts;
using FoodZOAI.UserManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodZOAI.UserManagement.Repositories
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

        public async Task<List<YearlyReminder>> GetActiveAsync()
        {
            return await _context.YearlyReminders
                .Where(r => r.IsActive && r.DeletedAt == null)
                .ToListAsync();
        }

        Task<IEnumerable<YearlyReminder>> IYearlyReminderRepository.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<YearlyReminder?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
