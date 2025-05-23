namespace FoodZOAI.UserManagement.DTOs
{
    public class YearlyReminderDTO
    {
        public int Id { get; set; }
        public int ReminderId { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
