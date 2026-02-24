using System;
using System.Collections.Generic;

namespace payrallproject.Models.Domains.Temp;

public partial class Leaf
{
    public int Id { get; set; }

    public int EmployeId { get; set; }

    public int Year { get; set; }

    public double AnnualLeavesAllocated { get; set; }

    public double AnnualLeavesUsed { get; set; }

    public double CasualLeavesAllocated { get; set; }

    public double CasualLeavesUsed { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public virtual Employe Employe { get; set; } = null!;
}
