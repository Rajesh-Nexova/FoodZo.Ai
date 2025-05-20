using System;

namespace FoodZOAI.UserManagement.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime? EmailVerifiedAt { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string AvatarUrl { get; set; }
        public string Status { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public DateTime? PasswordChangedAt { get; set; }
        public int FailedLoginAttempts { get; set; }
        public DateTime? LockedUntil { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public string TwoFactorSecret { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }

    
}