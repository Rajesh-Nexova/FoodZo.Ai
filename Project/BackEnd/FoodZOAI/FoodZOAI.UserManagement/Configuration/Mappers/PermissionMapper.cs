using FoodZOAI.UserManagement.Configuration.Contracts;
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;

public class PermissionMapper : IPermissionMapper
{
    public RoleDTO Map(Role source)
    {
        throw new NotImplementedException();
    }

    public PermissionDTO Map(Permission source)
    {
        throw new NotImplementedException();
    }

    public List<RoleDTO> MapList(List<Role> source)
    {
        throw new NotImplementedException();
    }

    public List<PermissionDTO> MapList(List<Permission> source)
    {
        throw new NotImplementedException();
    }

    public PermissionDTO ToDTO(Permission model)
    {
        return new PermissionDTO
        {
            Id = model.Id,
            Name = model.Name,
            Slug = model.Slug,
            Description = model.Description,
            Module = model.Module,
            Action = model.Action,
            Resource = model.Resource,
            IsSystemPermission = model.IsSystemPermission,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt
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
