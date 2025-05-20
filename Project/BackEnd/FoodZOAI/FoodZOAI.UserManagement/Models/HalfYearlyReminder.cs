using System;
using System.Collections.Generic;

namespace FoodZOAI.UserManagement.Models;

public partial class HalfYearlyReminder
{
    public int Id { get; set; }

    public int ReminderId { get; set; }

    public int Day { get; set; }

    public int Month { get; set; }

    public int Quarter { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool IsActive { get; set; }

    public virtual Reminder Reminder { get; set; } = null!;
}
