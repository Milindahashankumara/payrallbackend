using System;
using System.Collections.Generic;

namespace payrallproject.Models.Domains.Temp;

public partial class LeaveBalance
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public string LeaveType { get; set; } = null!;

    public int Year { get; set; }

    public decimal EntitledDays { get; set; }

    public decimal UsedDays { get; set; }

    public decimal BalanceDays { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual Employe Employee { get; set; } = null!;
}
