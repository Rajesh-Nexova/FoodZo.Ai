using System;
using System.Collections.Generic;

namespace FoodZOAI.UserManagement.Models;

public partial class User
{
    public int Id { get; set; }

    public int? OrganizationId { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime? EmailVerifiedAt { get; set; }

    public string PasswordHash { get; set; } = null!;

    public string? Salt { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Phone { get; set; }

    public string? AvatarUrl { get; set; }

    public string? Status { get; set; }

    public DateTime? LastLoginAt { get; set; }

    public DateTime? PasswordChangedAt { get; set; }

    public int? FailedLoginAttempts { get; set; }

    public DateTime? LockedUntil { get; set; }

    public bool? TwoFactorEnabled { get; set; }

    public string? TwoFactorSecret { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<User> InverseCreatedByNavigation { get; set; } = new List<User>();

    public virtual Organization? Organization { get; set; }

    public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();

    public virtual ICollection<UserPermission> UserPermissionGrantedByNavigations { get; set; } = new List<UserPermission>();

    public virtual ICollection<UserPermission> UserPermissionUsers { get; set; } = new List<UserPermission>();

    public virtual ICollection<UserProfile> UserProfiles { get; set; } = new List<UserProfile>();

    public virtual ICollection<UserRole> UserRoleAssignedByNavigations { get; set; } = new List<UserRole>();

    public virtual ICollection<UserRole> UserRoleUsers { get; set; } = new List<UserRole>();

    public virtual ICollection<UserSession> UserSessions { get; set; } = new List<UserSession>();
}
