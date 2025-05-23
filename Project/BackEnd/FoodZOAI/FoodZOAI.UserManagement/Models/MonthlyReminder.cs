namespace FoodZOAI.UserManagement.Models
{
    public class MonthlyReminder
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string Frequency { get; set; }
        public int DayOfMonth { get; set; }
        public DateTime StartDate { get; set; }
        public bool IsRepeated { get; set; }
        public bool IsEmailNotification { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsActive { get; set; }
        public string CreatedByUser { get; set; }
    }
}
