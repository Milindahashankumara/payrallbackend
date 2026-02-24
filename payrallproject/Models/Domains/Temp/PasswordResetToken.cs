using System;
using System.Collections.Generic;

namespace payrallproject.Models.Domains.Temp;

public partial class PasswordResetToken
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string? Token { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public virtual User? User { get; set; }
}
