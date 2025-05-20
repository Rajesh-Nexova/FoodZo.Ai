using System;

namespace FoodZOAI.UserManagement.Models
{
    public class ReminderNotificationDTO
    {
        public int Id { get; set; }
        public int ReminderId { get; set; }
        public string Subject { get; set; }
        public string? Description { get; set; }
        public DateTime FetchDateTime { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsEmailNotification { get; set; }
    }
}
