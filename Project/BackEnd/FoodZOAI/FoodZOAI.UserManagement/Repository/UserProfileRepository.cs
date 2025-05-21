using FoodZOAI.UserManagement.Contracts;
using FoodZOAI.UserManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodZOAI.UserManagement.Repository
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly FoodZoaiContext _context;

        public UserProfileRepository(FoodZoaiContext context)
        {
            _context = context;
        }

        public async Task<UserProfile> AddAsync(UserProfile profile)
        {
            _context.UserProfiles.Add(profile);
            await _context.SaveChangesAsync();
            return profile;
        }

        public async Task<UserProfile?> GetByUserIdAsync(int userId)
        {
            return await _context.UserProfiles
                .FirstOrDefaultAsync(p => p.UserId == userId);
        }

        public async Task<UserProfile> UpdateAsync(UserProfile profile)
        {
            _context.UserProfiles.Update(profile);
            await _context.SaveChangesAsync();
            return profile;
        }

        public async Task DeleteAsync(int userId)
        {
            var profile = await _context.UserProfiles
                .FirstOrDefaultAsync(p => p.UserId == userId);

            if (profile != null)
            {
                _context.UserProfiles.Remove(profile);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<UserProfile> UpdatePhotoAsync(int userId, string photoUrl)
        {
            var profile = await _context.UserProfiles
                .FirstOrDefaultAsync(p => p.UserId == userId);

            if (profile == null)
                throw new KeyNotFoundException("User profile not found.");

            // Assuming you're using CustomFields to store the photo URL or create a new field
            profile.CustomFields = photoUrl;
            profile.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return profile;
        }

        public async Task<IEnumerable<User>> GetAllUsersForDropdownAsync()
        {
            return await _context.Users
                .Where(u => u.DeletedAt == null)
                .Select(u => new User
                {
                    Id = u.Id,
                    Username = u.Username,
                    Email = u.Email
                })
                .ToListAsync();
        }

        public async Task<UserProfile> GetByIdAsync(int id)
        {
            return await _context.UserProfiles.FindAsync(id);
        }
    }
}
