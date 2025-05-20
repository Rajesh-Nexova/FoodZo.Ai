namespace FoodZOAI.UserManagement.DTOs
{
    public class AppsettingDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Key { get; set; } = null!;

        public string Value { get; set; } = null!;

        public string? CreatedByUser { get; set; }

        public string? ModifiedByUser { get; set; }

        public string? DeletedByUser { get; set; }

        public bool IsActive { get; set; }
    }
}
