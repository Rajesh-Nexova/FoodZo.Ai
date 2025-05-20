using System;
using System.Collections.Generic;

namespace FoodZOAI.UserManagement.Models;

public partial class ReminderNotification
{
    public int Id { get; set; }

    public int ReminderId { get; set; }

    public string Subject { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime FetchDateTime { get; set; }

    public bool IsDeleted { get; set; }

    public bool IsEmailNotification { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool IsActive { get; set; }

    public virtual Reminder Reminder { get; set; } = null!;
}
