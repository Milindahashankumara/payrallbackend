using System;
using System.Collections.Generic;

namespace payrallproject.Models.Domains.Temp;

public partial class Loan
{
    public int Id { get; set; }

    public int? EmployeId { get; set; }

    public decimal PrincipalAmount { get; set; }

    public int TermMonths { get; set; }

    public decimal MonthlyInstallment { get; set; }

    public decimal RemainingBalance { get; set; }

    public DateTime StartDate { get; set; }

    public bool IsActive { get; set; }

    public bool Settled { get; set; }

    public virtual Employe? Employe { get; set; }

    public virtual ICollection<Loanrepayment> Loanrepayments { get; set; } = new List<Loanrepayment>();
}
