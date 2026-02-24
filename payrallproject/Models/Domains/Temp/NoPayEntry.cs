using System;
using System.Collections.Generic;

namespace payrallproject.Models.Domains.Temp;

public partial class NoPayEntry
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public DateTime NoPayDate { get; set; }

    public string? Reason { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual Employe Employee { get; set; } = null!;
}
