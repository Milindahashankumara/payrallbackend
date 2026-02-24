using System;
using System.Collections.Generic;

namespace payrallproject.Models.Domains.Temp;

public partial class Leaves2
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public bool IsHalfDay { get; set; }

    public bool? IsFirstHalfDay { get; set; }

    public decimal NumberOfDays { get; set; }

    public string LeaveType { get; set; } = null!;

    public string? Reason { get; set; }

    public string Status { get; set; } = null!;

    public int Year { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual Employe Employee { get; set; } = null!;
}
