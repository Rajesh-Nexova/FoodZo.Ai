﻿namespace FoodZOAI.UserManagement.DTOs
{
    public class ChangePasswordRequestDTO
    {
        public int UserId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
