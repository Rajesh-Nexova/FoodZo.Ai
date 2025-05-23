namespace FoodZOAI.UserManagement.DTOs
{
    public class HalfYearlyReminderDTO
    {
        public int Id { get; set; }
        public int ReminderId { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Quarter { get; set; }
        public bool IsActive { get; set; }
    }
}
