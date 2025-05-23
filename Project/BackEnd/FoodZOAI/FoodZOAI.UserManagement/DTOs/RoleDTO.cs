namespace FoodZOAI.UserManagement.DTOs
{
    public class RoleDTO
    {
        public int Id { get; set; }

        public int? OrganizationId { get; set; }

        public string Name { get; set; } = null!;

        public string Slug { get; set; } = null!;

        public string? Description { get; set; }

        public int? Level { get; set; }

        public bool? IsSystemRole { get; set; }

        public string? Color { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
