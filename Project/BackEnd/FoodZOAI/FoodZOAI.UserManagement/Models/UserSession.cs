using System;
using System.Collections.Generic;

namespace FoodZOAI.UserManagement.Models;

public partial class UserSession
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string SessionToken { get; set; } = null!;

    public string? RefreshToken { get; set; }

    public string? IpAddress { get; set; }

    public string? UserAgent { get; set; }

    public string? DeviceInfo { get; set; }

    public string? Location { get; set; }

    public DateTime ExpiresAt { get; set; }

    public DateTime? LastActivityAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool IsActive { get; set; }

    public virtual User User { get; set; } = null!;
}
