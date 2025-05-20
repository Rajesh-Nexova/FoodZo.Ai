using FoodZOAI.UserManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodZOAI.UserManagement.Repositories
{
    public class ReminderNotificationRepository : IReminderNotificationRepository
    {
        private readonly FoodZoaiContext _context;

        public ReminderNotificationRepository(FoodZoaiContext context)
        {
            _context = context;
        }

        public async Task<ReminderNotification> GetByIdAsync(int id)
        {
            return await _context.ReminderNotifications
                                 .FirstOrDefaultAsync(rn => rn.Id == id);
        }

        public async Task<IEnumerable<ReminderNotification>> GetAllAsync()
        {
            return await _context.ReminderNotifications.ToListAsync();
        }

        public async Task AddAsync(ReminderNotification entity)
        {
            await _context.ReminderNotifications.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ReminderNotification entity)
        {
            _context.ReminderNotifications.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.ReminderNotifications.FindAsync(id);
            if (entity != null)
            {
                _context.ReminderNotifications.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
