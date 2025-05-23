namespace FoodZOAI.UserManagement.DTOs
{
    public class OneTimeReminderDTO
    {
        public int Id { get; set; }
        public string? Message { get; set; }
        public DateTime ReminderDate { get; set; }
        public bool IsActive { get; set; }
    }
}
