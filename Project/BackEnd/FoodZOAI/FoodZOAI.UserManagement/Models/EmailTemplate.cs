using System;
using System.Collections.Generic;

namespace FoodZOAI.UserManagement.Models;

public partial class EmailTemplate
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Subject { get; set; } = null!;

    public string Body { get; set; } = null!;

    public bool IsActive { get; set; }

    public string? CreatedByUser { get; set; }

    public string? ModifiedByUser { get; set; }

    public string? DeletedByUser { get; set; }
}
