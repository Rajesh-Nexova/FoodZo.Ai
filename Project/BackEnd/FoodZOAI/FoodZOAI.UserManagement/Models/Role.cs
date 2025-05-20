using System;
using System.Collections.Generic;

namespace FoodZOAI.UserManagement.Models;

public partial class Role
{
    public int Id { get; set; }

    public int? OrganizationId { get; set; }

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string? Description { get; set; }

    public int? Level { get; set; }

    public bool? IsSystemRole { get; set; }

    public string? Color { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Organization? Organization { get; set; }

    public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
