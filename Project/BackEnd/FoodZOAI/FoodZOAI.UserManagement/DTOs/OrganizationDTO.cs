namespace FoodZOAI.UserManagement.DTOs
{
    public class OrganizationDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Slug { get; set; } = null!;

        public string? Description { get; set; }

        public string? Website { get; set; }

        public string? LogoUrl { get; set; }

        public string? Banner_Image { get; set; }

        public string? SubscriptionPlan { get; set; }

        public int? MaxUsers { get; set; }

        public string? Status { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
