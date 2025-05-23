namespace FoodZOAI.UserManagement.DTOs
{
    public class UserSessionDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string SessionToken { get; set; } = null!;
        public string? RefreshToken { get; set; }
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
        public string? DeviceInfo { get; set; }
        public string? Location { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime? LastActivityAt { get; set; }
        public bool IsActive { get; set; }
    }
}
