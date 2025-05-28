using FoodZOAI.UserManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodZOAI.Repositories;

public class UserSessionRepository : IUserSessionRepository
{
    private readonly FoodZoaiContext _context;

    public UserSessionRepository(FoodZoaiContext context)
    {
        _context = context;
    }

    public async Task<List<UserSession>> GetAllAsync() =>
        await _context.UserSessions.ToListAsync();

    public async Task<UserSession?> GetByIdAsync(int id) =>
        await _context.UserSessions.FindAsync(id);

    public async Task<UserSession> AddAsync(UserSession session)
    {
        _context.UserSessions.Add(session);
        await _context.SaveChangesAsync();
        return session;
    }

    public async Task<UserSession> UpdateAsync(UserSession session)
    {
        _context.UserSessions.Update(session);
        await _context.SaveChangesAsync();
        return session;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.UserSessions.FindAsync(id);
        if (entity == null) return false;

        _context.UserSessions.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}
