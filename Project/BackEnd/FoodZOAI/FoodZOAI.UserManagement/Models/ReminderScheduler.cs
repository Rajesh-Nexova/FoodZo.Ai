using System;
using System.Collections.Generic;

namespace FoodZOAI.UserManagement.Models;

public partial class ReminderScheduler
{
    public int Id { get; set; }

    public int Duration { get; set; }

    public bool IsActive { get; set; }

    public string? Frequency { get; set; }

    public DateTime CreatedDate { get; set; }

    public int UserId { get; set; }

    public bool IsRead { get; set; }

    public bool IsEmailNotification { get; set; }

    public string Subject { get; set; } = null!;

    public string? Message { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool IsActive1 { get; set; }

    public virtual User User { get; set; } = null!;
    public int ReminderId { get; internal set; }
}
