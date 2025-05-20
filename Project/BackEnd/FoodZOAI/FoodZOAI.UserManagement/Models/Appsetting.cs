using System;
using System.Collections.Generic;

namespace FoodZOAI.UserManagement.Models;

public partial class Appsetting
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
