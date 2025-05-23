using System;

namespace FoodZOAI.UserManagement.DTOs
{
    public class MonthlyReminderDTO
    {
        public int Id { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Frequency { get; set; } = "Monthly";
        public int DayOfMonth { get; set; }
        public DateTime StartDate { get; set; }
        public bool IsRepeated { get; set; }
        public bool IsEmailNotification { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsActive { get; set; }
        public string CreatedByUser { get; set; } = string.Empty;
    }
}
