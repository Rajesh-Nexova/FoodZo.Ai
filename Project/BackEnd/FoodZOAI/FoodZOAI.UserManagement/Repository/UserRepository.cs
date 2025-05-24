using FoodZOAI.UserManagement.Contracts;
using FoodZOAI.UserManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodZOAI.UserManagement.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly FoodZoaiContext _context;

        public UserRepository(FoodZoaiContext context)
        {
            _context = context;
        }

        public async Task<User> AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                throw new KeyNotFoundException($"User with ID {id} not found.");
            return user;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<IEnumerable<User>> GetPaginatedUsersAsync(int pageNumber, int pageSize)
        {
            return await _context.Users
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<User>> GetRecentlyRegisteredUsersAsync(int count)
        {
            return await _context.Users
                .OrderByDescending(u => u.CreatedAt)
                .Take(count)
                .ToListAsync();
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username && u.DeletedAt == null);
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task UpdateAsync(object user)
        {
            if (user is not User u)
                throw new ArgumentException("Invalid user object", nameof(user));

            _context.Users.Update(u);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ChangePasswordAsync(int userId, string newPasswordHash, string newSalt)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return false;

            user.PasswordHash = newPasswordHash;
            user.Salt = newSalt;
            user.PasswordChangedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task GetByIdAsync(object userId)
        {
            if (userId is not int id)
                throw new ArgumentException("Invalid user ID type", nameof(userId));

            var user = await _context.Users.FindAsync(id);
            if (user == null)
                throw new KeyNotFoundException($"User with ID {id} not found.");

            // No return because this method returns Task (void), not Task<User>
        }

        public async Task<IEnumerable<User>> GetUsersOrderedByCreationDateAsync(int count)
        {
            return await _context.Users
                .Where(u => u.DeletedAt == null) // Optional: exclude deleted users
                .OrderByDescending(u => u.CreatedAt)
                .Take(count)
                .ToListAsync();
        }

        public async Task<User> GetByUserIdAsync(int userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }

        

        public async Task<int> GetTotalUserCountAsync()
        {
            return await _context.Users
                .Where(u => u.DeletedAt == null)
                .CountAsync();
        }

        public async Task<IEnumerable<User>> GetUsersRegisteredAfterAsync(DateTime fromDate)
        {
            return await _context.Users
                .Where(u => u.CreatedAt >= fromDate && u.DeletedAt == null)
                .OrderByDescending(u => u.CreatedAt)
                .ToListAsync();
        }

       
    }
}
