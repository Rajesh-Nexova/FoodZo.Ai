using FoodZOAI.UserManagement.Models;
using FoodZOAI.UserManagement.DTOs;

namespace FoodZOAI.UserManagement.Configuration.Contracts
{
    public interface IUserMapper
    {
        UserDTO MapToDTO(User user);
        User MapToEntity(UserDTO userDto);
        User MapToEntity(UserDTO userDto, User existingUser); // For updates
    }
}
