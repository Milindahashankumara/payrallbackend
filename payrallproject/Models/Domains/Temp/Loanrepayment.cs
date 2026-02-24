using System;
using System.Collections.Generic;

namespace payrallproject.Models.Domains.Temp;

public partial class Loanrepayment
{
    public int Id { get; set; }

    public int? LoanId { get; set; }

    public int MonthNo { get; set; }

    public DateTime PaymentDate { get; set; }

    public decimal InstallmentAmount { get; set; }

    public decimal RemainingBalance { get; set; }

    public string Description { get; set; } = null!;

    public virtual Loan? Loan { get; set; }
}
