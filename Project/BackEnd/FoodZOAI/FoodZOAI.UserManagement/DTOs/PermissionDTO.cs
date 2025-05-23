namespace FoodZOAI.UserManagement.DTOs
{
    public class PermissionDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Slug { get; set; } = null!;

        public string? Description { get; set; }

        public string? Module { get; set; }

        public string? Action { get; set; }

        public string? Resource { get; set; }

        public bool? IsSystemPermission { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
