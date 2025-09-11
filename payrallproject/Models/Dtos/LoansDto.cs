using payrallproject.Models.Domains;
using payrallproject.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace payrallproject.Models.Dtos
{
    public class LoansDto
    {
        public int? EmployeID { get; set; }
        public decimal PrincipalAmount { get; set; }
        public decimal InterestRate { get; set; }
        public int TermMonths { get; set; }

        public LoanType LoanType { get; set; }
        public decimal MonthlyInstallment { get; set; }

        public decimal RemainingBalance { get; set; }
        public DateTime StartDate { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
