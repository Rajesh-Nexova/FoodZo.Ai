using System;
using System.Collections.Generic;

namespace FoodZOAI.UserManagement.Models;

public partial class UserProfile
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string? Bio { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? Gender { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? StateProvince { get; set; }

    public string? PostalCode { get; set; }

    public string? Country { get; set; }

    public string? Timezone { get; set; }

    public string? Language { get; set; }

    public string? NotificationPreferences { get; set; }

    public string? PrivacySettings { get; set; }

    public string? CustomFields { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
