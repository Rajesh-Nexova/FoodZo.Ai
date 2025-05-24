using System.Collections.Generic;
using System.Linq;
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Configuration.Contracts;
using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement.Configuration.Mappers
{
    public class UserProfileMapper : IUserProfileMapper
    {
        public List<UserProfile> ListMapToDomain(List<UserProfileDTO> dtoList)
        {
            return dtoList?.Select(MapToDomain).ToList() ?? new List<UserProfile>();
        }

        public UserProfileDTO Map(UserProfile source)
        {
            return new UserProfileDTO
            {
                Id = source.Id,
                UserId = source.UserId,
                Bio = source.Bio,
                DateOfBirth = source.DateOfBirth,
                Gender = source.Gender,
                Address = source.Address,
                City = source.City,
                StateProvince = source.StateProvince,
                PostalCode = source.PostalCode,
                Country = source.Country,
                Timezone = source.Timezone,
                Language = source.Language,
                NotificationPreferences = source.NotificationPreferences,
                PrivacySettings = source.PrivacySettings,
                CustomFields = source.CustomFields,
                CreatedAt = source.CreatedAt,
                UpdatedAt = source.UpdatedAt
            };
        }

        public List<UserProfileDTO> MapList(List<UserProfile> source)
        {
            return source.Select(Map).ToList();
        }

       

        

        

        public UserProfile MapToDomain(UserProfileDTO dto)
        {
            return new UserProfile
            {
                Id = dto.Id,
                UserId = dto.UserId,
                Bio = dto.Bio,
                DateOfBirth = dto.DateOfBirth,
                Gender = dto.Gender,
                Address = dto.Address,
                City = dto.City,
                StateProvince = dto.StateProvince,
                PostalCode = dto.PostalCode,
                Country = dto.Country,
                Timezone = dto.Timezone,
                Language = dto.Language,
                NotificationPreferences = dto.NotificationPreferences,
                PrivacySettings = dto.PrivacySettings,
                CustomFields = dto.CustomFields,
                CreatedAt = dto.CreatedAt,
                UpdatedAt = dto.UpdatedAt
            };
        }

        
    }
}
