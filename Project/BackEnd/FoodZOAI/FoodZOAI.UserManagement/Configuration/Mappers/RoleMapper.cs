using FoodZOAI.UserManagement.Configuration.Contracts;
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement.Configuration.Mappers
{
    public class RoleMapper : IRoleMapper
    {
        public RoleDTO Map(Role source)
        {
            if (source == null) return null!;

            return new RoleDTO
            {
                Id = source.Id,
                OrganizationId = source.OrganizationId,
                Name = source.Name,
                Slug = source.Slug,
                Description = source.Description,
                Level = source.Level,
                IsSystemRole = source.IsSystemRole,
                Color = source.Color,
                CreatedAt = source.CreatedAt,
                UpdatedAt = source.UpdatedAt
            };
        }

        public List<RoleDTO> MapList(List<Role> source)
        {
            return source?.Select(Map).ToList() ?? new List<RoleDTO>();
        }

        public Role MapToDomain(RoleDTO dto)
        {
            if (dto == null) return null!;

            return new Role
            {
                Id = dto.Id,
                OrganizationId = dto.OrganizationId,
                Name = dto.Name,
                Slug = dto.Slug,
                Description = dto.Description,
                Level = dto.Level,
                IsSystemRole = dto.IsSystemRole,
                Color = dto.Color,
                CreatedAt = dto.CreatedAt,
                UpdatedAt = dto.UpdatedAt
            };
        }

        public List<Role> ListMapToDomain(List<RoleDTO> dtoList)
        {
            return dtoList?.Select(MapToDomain).ToList() ?? new List<Role>();
        }



        public Role MapToEntity(RoleDTO dto, Role existingEntity)
        {
            if (dto == null || existingEntity == null) return existingEntity;

            existingEntity.OrganizationId = dto.OrganizationId;
            existingEntity.Name = dto.Name;
            existingEntity.Slug = dto.Slug;
            existingEntity.Description = dto.Description;
            existingEntity.Level = dto.Level;
            existingEntity.IsSystemRole = dto.IsSystemRole;
            existingEntity.Color = dto.Color;
            existingEntity.CreatedAt = dto.CreatedAt;
            existingEntity.UpdatedAt = dto.UpdatedAt;

            return existingEntity;
        }

        // These are likely irrelevant here — based on RoleMapper responsibility
        public UserDTO MapToDTO(User entity)
        {
            throw new NotImplementedException("This method doesn't belong in RoleMapper.");
        }

        public void MapToDTOList(List<User> users)
        {
            throw new NotImplementedException("This method doesn't belong in RoleMapper.");
        }
    }
}
