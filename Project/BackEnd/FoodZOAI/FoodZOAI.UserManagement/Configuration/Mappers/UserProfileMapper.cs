using System.Collections.Generic;
using System.Linq;
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Configuration.Contracts;
using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement.Configuration.Mappers
{
    public class UserProfileMapper : IMapperService<UserProfile, UserProfileDTO>
    {
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

        public UserProfile MapToEntity(UserProfileDTO dto)
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

        public UserProfile MapToEntity(UserProfileDTO dto, UserProfile existingEntity)
        {
            existingEntity.UserId = dto.UserId;
            existingEntity.Bio = dto.Bio;
            existingEntity.DateOfBirth = dto.DateOfBirth;
            existingEntity.Gender = dto.Gender;
            existingEntity.Address = dto.Address;
            existingEntity.City = dto.City;
            existingEntity.StateProvince = dto.StateProvince;
            existingEntity.PostalCode = dto.PostalCode;
            existingEntity.Country = dto.Country;
            existingEntity.Timezone = dto.Timezone;
            existingEntity.Language = dto.Language;
            existingEntity.NotificationPreferences = dto.NotificationPreferences;
            existingEntity.PrivacySettings = dto.PrivacySettings;
            existingEntity.CustomFields = dto.CustomFields;
            existingEntity.UpdatedAt = DateTime.UtcNow;

            return existingEntity;
        }
    }
}
