namespace FoodZOAI.UserManagement.DTOs
{
    public class EmailSettingDTO
    {
        public int Id { get; set; }

        public string Host { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public string Password { get; set; } = null!;

        public bool IsEnableSsl { get; set; }

        public bool IsDefault { get; set; }

        public bool IsActive { get; set; }

        public string? CreatedByUser { get; set; }

        public string? ModifiedByUser { get; set; }

        public string? DeletedByUser { get; set; }
    }
}
