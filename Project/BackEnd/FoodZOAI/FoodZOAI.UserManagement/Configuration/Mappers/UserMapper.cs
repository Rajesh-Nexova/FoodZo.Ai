using System;
using System.Collections.Generic;
using System.Linq;
using FoodZOAI.UserManagement.Configuration.Contracts;
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement.Configuration.Mappers
{
    public class UserMapper : IUserMapper
    {
        public List<User> ListMapToDomain(List<UserDTO> dtoList)
        {
            return dtoList?.Select(MapToDomain).ToList() ?? new List<User>();
        }

        public UserDTO Map(User source)
        {
            if (source == null)
                return new UserDTO();

            return new UserDTO
            {
                Id = source.Id,
                OrganizationId = source.OrganizationId ?? 0,
                Username = source.Username,
                Email = source.Email,
                EmailVerifiedAt = source.EmailVerifiedAt,
                PasswordHash = source.PasswordHash,
                Salt = source.Salt,
                FirstName = source.FirstName,
                LastName = source.LastName,
                Phone = source.Phone,
                AvatarUrl = source.AvatarUrl,
                Status = source.Status,
                LastLoginAt = source.LastLoginAt,
                PasswordChangedAt = source.PasswordChangedAt,
                FailedLoginAttempts = source.FailedLoginAttempts ?? 0,
                LockedUntil = source.LockedUntil,
                TwoFactorEnabled = source.TwoFactorEnabled ?? false,
                TwoFactorSecret = source.TwoFactorSecret,
                CreatedBy = source.CreatedBy,
                CreatedAt = source.CreatedAt ?? DateTime.UtcNow,
                UpdatedAt = source.UpdatedAt,
                DeletedAt = source.DeletedAt

            };
        }

        public List<UserDTO> MapList(List<User> source)
        {
            return source?.Select(Map).ToList() ?? new List<UserDTO>();
        }

        public User MapToDomain(UserDTO dto)
        {
            
            if (dto == null)
                return new User();

            return new User
            {

                Id = dto.Id,
                OrganizationId = dto.OrganizationId ,
                Username = dto.Username,
                Email = dto.Email,
                EmailVerifiedAt = dto.EmailVerifiedAt,
                PasswordHash = dto.PasswordHash,
                Salt = dto.Salt,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Phone = dto.Phone,
                AvatarUrl = dto.AvatarUrl,
                Status = dto.Status,
                LastLoginAt = dto.LastLoginAt,
                PasswordChangedAt = dto.PasswordChangedAt,
                FailedLoginAttempts = dto.FailedLoginAttempts ,
                LockedUntil = dto.LockedUntil,
                TwoFactorEnabled = dto.TwoFactorEnabled ,
                TwoFactorSecret = dto.TwoFactorSecret,
                CreatedBy = dto.CreatedBy,
                CreatedAt = dto.CreatedAt ,
                UpdatedAt = dto.UpdatedAt,
                DeletedAt = dto.DeletedAt
            };
        }
    }
    }

