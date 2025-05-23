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
        public Task AddAsync(DailyReminder entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DailyReminder>> GetAllAsync()
        {

            return await _context.DailyReminders.ToListAsync();
        }

        public  async Task<DailyReminder> GetByIdAsync(int id)
        {
            return await _context.DailyReminders.FindAsync(id);
        }

        public Task UpdateAsync(DailyReminder entity)
        {
            throw new NotImplementedException();
        }
    }
}
