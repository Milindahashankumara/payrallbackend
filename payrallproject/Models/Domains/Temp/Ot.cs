using System;
using System.Collections.Generic;

namespace payrallproject.Models.Domains.Temp;

public partial class Ot
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public decimal? Rate { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<EmployeeOvertime> EmployeeOvertimes { get; set; } = new List<EmployeeOvertime>();
}
