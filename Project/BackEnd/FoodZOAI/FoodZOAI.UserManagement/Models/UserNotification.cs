using System;
using System.Collections.Generic;

namespace FoodZOAI.UserManagement.Models
{

    public partial class UserNotification
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Message { get; set; } = null!;

        public bool IsRead { get; set; }

        public int NotificationsType { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public bool IsActive { get; set; }

        public virtual User User { get; set; } = null!;
    }
}

