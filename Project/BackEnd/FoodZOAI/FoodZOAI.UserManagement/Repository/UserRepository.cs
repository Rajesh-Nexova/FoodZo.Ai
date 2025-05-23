using System;
using FoodZOAI.UserManagement.Models;
using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{
    private readonly FoodZoaiContext _context;

    public UserRepository(FoodZoaiContext context)
    {
        _context = context;
    }

    public async Task<int> GetActiveUserCountAsync()
    {
        return await _context.Users.CountAsync(u => u.IsActive == true);

    }

    public async Task<int> GetInactiveUserCountAsync()
    {
        return await _context.Users.CountAsync(u => u.IsActive == false);
    }

    public async Task<List<User>> GetOnlineUsersAsync()
    {
        return await _context.Users
    .Where(u => u.IsOnline == true && u.IsActive == true)
    .ToListAsync();
    }

    public async Task<int> GetTotalUserCountAsync()
    {
        return await _context.Users.CountAsync();
    }



}
