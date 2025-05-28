namespace FoodZOAI.UserManagement.DTOs
{
    public class ResetPasswordRequestDTO
    {
        public int UserId { get; set; }
        public string NewPassword { get; set; }
    }
}
