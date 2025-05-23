namespace FoodZOAI.UserManagement.Models
{
     public class WeeklyReminder
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
            public DateTime? ModifiedAt { get; set; }
            public DateTime? DeletedAt { get; set; }
            public bool IsActive { get; set; }
            public string? CreatedByUser { get; set; }
            public string? ModifiedByUser { get; set; }
            public string? DeletedByUser { get; set; }
        }
    }

