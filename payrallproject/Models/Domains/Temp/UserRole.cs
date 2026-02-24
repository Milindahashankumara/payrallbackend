using System;
using System.Collections.Generic;

namespace payrallproject.Models.Domains.Temp;

public partial class UserRole
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? RolesId { get; set; }

    public virtual Role? Roles { get; set; }

    public virtual User? User { get; set; }
}
