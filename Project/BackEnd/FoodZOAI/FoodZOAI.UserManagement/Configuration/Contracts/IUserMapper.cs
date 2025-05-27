using FoodZOAI.UserManagement.Models;
using FoodZOAI.UserManagement.DTOs;

namespace FoodZOAI.UserManagement.Configuration.Contracts
{
    public interface IUserMapper : IMapperServices<User, UserDTO>
    {
        User MapToDomain(UserDTO dto);
        List<User> ListMapToDomain(List<UserDTO> dtoList);
    }
}
