using System;
using FoodZOAI.UserManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodZOAI.UserManagement.Repositories
{
    public class UserSessionRepository : IUserSessionRepository
    {
        private readonly FoodZoaiContext _context;

        public UserSessionRepository(FoodZoaiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserSession>> GetAllAsync()
        {
            return await _context.UserSessions
                                 .Where(s => s.IsActive)
                                 .ToListAsync();
        }

        public async Task<UserSession?> GetByIdAsync(int id)
        {
            return await _context.UserSessions
                                 .FirstOrDefaultAsync(s => s.Id == id && s.IsActive);
        }

        public async Task AddAsync(UserSession session)
        {
            session.CreatedAt = DateTime.UtcNow;
            session.IsActive = true;

            await _context.UserSessions.AddAsync(session);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserSession session)
        {
            session.UpdatedAt = DateTime.UtcNow;
            _context.UserSessions.Update(session);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var session = await _context.UserSessions.FindAsync(id);
            if (session != null)
            {
                session.IsActive = false;
                session.DeletedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }
    }
}
