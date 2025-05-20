using System;
using System.Collections.Generic;

namespace FoodZOAI.UserManagement.Models;

public partial class UserPermission
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int PermissionId { get; set; }

    public bool? Granted { get; set; }

    public int? GrantedBy { get; set; }

    public DateTime? GrantedAt { get; set; }

    public DateTime? ExpiresAt { get; set; }

    public virtual User? GrantedByNavigation { get; set; }

    public virtual Permission Permission { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
