using FoodZOAI.UserManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodZOAI.UserManagement.Repositories
{
    public class ReminderSchedulerRepository : IReminderSchedulerRepository
    {
        private readonly FoodZoaiContext _context;

        public ReminderSchedulerRepository(FoodZoaiContext context)
        {
            _context = context;
        }

        public async Task<ReminderScheduler> GetByIdAsync(int id)
        {
            return await _context.ReminderSchedulers
                                 .FirstOrDefaultAsync(rs => rs.Id == id);
        }

        public async Task<IEnumerable<ReminderScheduler>> GetAllAsync()
        {
            return await _context.ReminderSchedulers.ToListAsync();
        }

        public async Task AddAsync(ReminderScheduler entity)
        {
            await _context.ReminderSchedulers.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ReminderScheduler entity)
        {
            _context.ReminderSchedulers.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.ReminderSchedulers.FindAsync(id);
            if (entity != null)
            {
                _context.ReminderSchedulers.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
