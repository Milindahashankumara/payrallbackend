using System;
using System.Collections.Generic;

namespace payrallproject.Models.Domains.Temp;

public partial class NoPayDay
{
    public int Id { get; set; }

    public int EmployeId { get; set; }

    public DateTime Date { get; set; }

    public string Reason { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public virtual Employe Employe { get; set; } = null!;
}
