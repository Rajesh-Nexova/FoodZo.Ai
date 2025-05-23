using FoodZOAI.UserManagement.Configuration.Contracts;
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement.Configuration.Mappers
{
    public class PermissionMapper : IPermissionMapper
    {
        public PermissionDTO ToDTO(Permission entity)
        {
            return new PermissionDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                Slug = entity.Slug,
                Description = entity.Description,
                Module = entity.Module,
                Action = entity.Action,
                Resource = entity.Resource,
                IsSystemPermission = entity.IsSystemPermission,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public Permission ToEntity(PermissionDTO dto)
        {
            return new Permission
            {
                Id = dto.Id,
                Name = dto.Name,
                Slug = dto.Slug,
                Description = dto.Description,
                Module = dto.Module,
                Action = dto.Action,
                Resource = dto.Resource,
                IsSystemPermission = dto.IsSystemPermission,
                CreatedAt = dto.CreatedAt,
                UpdatedAt = dto.UpdatedAt
            };
        }
    }

}
