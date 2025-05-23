namespace FoodZOAI.UserManagement.Models
{
    public class OneTimeReminder
    {
        public int Id { get; set; }
        public string? Message { get; set; }
        public DateTime ReminderDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
