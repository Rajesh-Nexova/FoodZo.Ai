using FoodZOAI.UserManagement.Contracts;
using FoodZOAI.UserManagement.Models;
using Microsoft.EntityFrameworkCore;
using FoodZOAI.UserManagement.Configuration.Contracts;

namespace FoodZOAI.UserManagement.Repository
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly FoodZoaiContext _context;

        public PermissionRepository(FoodZoaiContext context)
        {
            _context = context;
        }

        public async Task<List<Permission>> GetAllAsync()
        {
            return await _context.Permissions.ToListAsync();
        }

        public async Task<Permission?> GetByIdAsync(int id)
        {
            return await _context.Permissions.FindAsync(id);
        }

        public async Task<Permission> AddAsync(Permission permission)
        {
            await _context.Permissions.AddAsync(permission);
            await _context.SaveChangesAsync();
            return permission;
        }

        public async Task<Permission> UpdateAsync(Permission permission)
        {
            _context.Permissions.Update(permission);
            await _context.SaveChangesAsync();
            return permission;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var permission = await _context.Permissions.FindAsync(id);
            if (permission == null)
                return false;

            _context.Permissions.Remove(permission);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Permissions.AnyAsync(p => p.Id == id);
        }
    }
}
