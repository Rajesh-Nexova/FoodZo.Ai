using System;
using System.Collections.Generic;

namespace FoodZOAI.UserManagement.Models;

public partial class EmailSetting
{
    public int Id { get; set; }

    public string Host { get; set; } = null!;

    public int Port { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool IsEnableSsl { get; set; }

    public bool IsDefault { get; set; }

    public bool IsActive { get; set; }

    public string? CreatedByUser { get; set; }

    public string? ModifiedByUser { get; set; }

    public string? DeletedByUser { get; set; }

   
}
