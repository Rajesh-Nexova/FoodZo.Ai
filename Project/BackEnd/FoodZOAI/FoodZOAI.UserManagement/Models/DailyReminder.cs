using System;
using System.Collections.Generic;

namespace FoodZOAI.UserManagement.Models;

public partial class DailyReminder
{
    public int Id { get; set; }

    public int ReminderId { get; set; }

    public string? DayOfWeek { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool IsActive { get; set; }

    public virtual Reminder Reminder { get; set; } = null!;
}
