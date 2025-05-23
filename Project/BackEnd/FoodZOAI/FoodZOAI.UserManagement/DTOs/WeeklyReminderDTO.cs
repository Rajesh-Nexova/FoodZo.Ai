
    namespace FoodZOAI.UserManagement.DTOs
{
  
        public class WeeklyReminderDTO
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
            public bool IsActive { get; set; }
        }
    }

