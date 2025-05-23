using FoodZOAI.UserManagement.Configuration.Contracts;
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement.Configuration.Mappers
{
    public class RoleMapper : IRoleMapper
    {


        public RoleDTO MapToDTO(Role dto)
        {
            return new RoleDTO
            {
                Id = dto.Id,
                Name = dto.Name,
                OrganizationId = dto.OrganizationId,
                Slug = dto.Slug,
                Description = dto.Description,
                Level = dto.Level,
                  IsSystemRole = dto.IsSystemRole,
                  Color = dto.Color,
                  CreatedAt = dto.CreatedAt,
                  UpdatedAt = dto.UpdatedAt

                 
            };

        }

        public Role MapToDomain(RoleDTO roleDto)
        {
            return new Role
            {
                Id = roleDto.Id,
                Name = roleDto.Name,
                OrganizationId = roleDto.OrganizationId,
                Slug = roleDto.Slug,
                Description = roleDto.Description,
                Level = roleDto.Level,
                IsSystemRole = roleDto.IsSystemRole,
                Color = roleDto.Color,
                CreatedAt = roleDto.CreatedAt,
                UpdatedAt = roleDto.UpdatedAt

            };
        }
    }
}
