namespace FoodZOAI.UserManagement.Models
{
    public class UserNotificationDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public int NotificationsType { get; set; }  // 0 = REMINDER, 1 = SHARE_USER
    }
}
