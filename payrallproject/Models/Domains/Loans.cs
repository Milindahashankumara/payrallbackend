using payrallproject.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace payrallproject.Models.Domains
{
    public class Loans
    {
        [Key]
        public int? Id { get; set; }
        public int? EmployeID { get; set; }
        public decimal PrincipalAmount { get; set; }
        public decimal InterestRate { get; set; }
        public int TermMonths { get; set; }

        public LoanType LoanType { get; set; }
        public decimal MonthlyInstallment { get; set; }

        public decimal RemainingBalance { get; set; }
        public DateTime StartDate { get; set; }
        public bool IsActive { get; set; } = true;

        public Employe Employe { get; set; }
        public ICollection<LoanRepayment> Repayments { get; set; }
    }
}
