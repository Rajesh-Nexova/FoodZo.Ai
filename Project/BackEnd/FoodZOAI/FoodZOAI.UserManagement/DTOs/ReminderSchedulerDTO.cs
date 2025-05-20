using System;

namespace FoodZOAI.UserManagement.Models
{
    public class ReminderSchedulerDTO
    {
        public int Id { get; set; }
        public int Duration { get; set; }
        public bool IsActive { get; set; }
        public string Frequency { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; }
        public int ReminderId { get; set; }
        public bool IsRead { get; set; }
        public bool IsEmailNotification { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
