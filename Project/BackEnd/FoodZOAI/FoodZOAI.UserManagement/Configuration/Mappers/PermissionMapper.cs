using FoodZOAI.UserManagement.Configuration.Contracts;
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;

public class PermissionMapper : IPermissionMapper
{
    public PermissionDTO Map(Permission source)
    {
        if (source == null) return null!;
        return new PermissionDTO
        {
            Id = source.Id,
            Name = source.Name,
            Description = source.Description
        };
    }

    public List<PermissionDTO> MapList(List<Permission> source)
    {
        return source?.Select(Map).ToList() ?? new List<PermissionDTO>();
    }

    public Permission MapToDomain(PermissionDTO dto)
    {
        if (dto == null) return null!;
        return new Permission
        {
            Id = dto.Id,
            Name = dto.Name,
            Description = dto.Description
        };
    }

    public List<Permission> ListMapToDomain(List<PermissionDTO> dtoList)
    {
        return dtoList?.Select(MapToDomain).ToList() ?? new List<Permission>();
    }
}
