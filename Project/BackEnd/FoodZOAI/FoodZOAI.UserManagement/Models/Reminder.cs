using System;
using System.Collections.Generic;

namespace FoodZOAI.UserManagement.Models;

public class Reminder
{
    public int Id { get; set; }
    public string Subject { get; set; }
    public string? Message { get; set; }
    public string Frequency { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? DayOfWeek { get; set; }
    public bool IsRepeated { get; set; }
    public bool IsEmailNotification { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }            // <-- Add this
    public DateTime? DeletedAt { get; set; }             // <-- Add this
    public bool IsActive { get; set; }
    public string? CreatedByUser { get; set; }           // <-- Add this
    public string? ModifiedByUser { get; set; }          // <-- Add this
    public string? DeletedByUser { get; set; }           // <-- Add this


    public virtual ICollection<DailyReminder> DailyReminders { get; set; } = new List<DailyReminder>();

    public virtual ICollection<HalfYearlyReminder> HalfYearlyReminders { get; set; } = new List<HalfYearlyReminder>();

    public virtual ICollection<QuarterlyReminder> QuarterlyReminders { get; set; } = new List<QuarterlyReminder>();

    public virtual ICollection<ReminderNotification> ReminderNotifications { get; set; } = new List<ReminderNotification>();

    public virtual ICollection<ReminderUser> ReminderUsers { get; set; } = new List<ReminderUser>();
}
