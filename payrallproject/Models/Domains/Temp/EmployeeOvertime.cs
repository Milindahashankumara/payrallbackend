using System;
using System.Collections.Generic;

namespace payrallproject.Models.Domains.Temp;

public partial class EmployeeOvertime
{
    public int Id { get; set; }

    public int? EmployeId { get; set; }

    public int? OtId { get; set; }

    public DateTime? DateWorked { get; set; }

    public int? HoursWorked { get; set; }

    public string? Remarks { get; set; }

    public decimal? Amount { get; set; }

    public virtual Employe? Employe { get; set; }

    public virtual Ot? Ot { get; set; }
}
