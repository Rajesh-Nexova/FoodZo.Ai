using System.Threading.Tasks;
using System.Collections.Generic;
using FoodZOAI.UserManagement.Models;
using FoodZOAI.UserManagement.DTOs;
using Microsoft.AspNetCore.Http;

namespace FoodZOAI.UserManagement.Configuration.Contracts
{
    public interface IUserProfileMapper : IMapperServices<UserProfile, UserProfileDTO>
    {
        UserProfile MapToDomain(UserProfileDTO dto);
        //UserProfile MapToDomain(UserProfileDTO profileDto, UserProfile profile);
        List<UserProfile> ListMapToDomain(List<UserProfileDTO> dtoList);
    }
}