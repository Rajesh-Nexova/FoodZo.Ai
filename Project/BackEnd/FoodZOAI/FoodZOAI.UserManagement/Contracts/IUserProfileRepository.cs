using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement.Contracts
{
    public interface IUserProfileRepository
    {

        Task<UserProfile> UpdateAsync(UserProfile profile);
        Task<UserProfile> UpdatePhotoAsync(int userId, string photoUrl);
        Task<IEnumerable<User>> GetAllUsersForDropdownAsync();

        Task<UserProfile> GetByIdAsync(int id);
    }
}
