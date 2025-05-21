namespace FoodZOAI.UserManagement.DTOs
{
    public class SendEmailDTO
    {
        public List<string> To { get; set; } = new();
        public List<string>? Cc { get; set; }
        public List<string>? Bcc { get; set; }

        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsHtml { get; set; } = true;
        public string? FromName { get; set; }
    }
}
