using payrallproject.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace payrallproject.Models.Dtos
{
    public class LoansDto
    {
        [Required(ErrorMessage = "Employee ID is required")]
        public int? EmployeID { get; set; }

        [Required(ErrorMessage = "Principal amount is required")]
        [Range(1, double.MaxValue, ErrorMessage = "Principal amount must be greater than 0")]
        public decimal PrincipalAmount { get; set; }

        [Required(ErrorMessage = "Interest rate is required")]
        [Range(0.01, 100, ErrorMessage = "Interest rate must be between 0.01% and 100%")]
        public decimal InterestRate { get; set; }

        [Required(ErrorMessage = "Loan term is required")]
        [Range(1, 360, ErrorMessage = "Loan term must be between 1 and 360 months")]
        public int TermMonths { get; set; }

        [Required(ErrorMessage = "Loan type is required")]
        public LoanType LoanType { get; set; }

        public DateTime StartDate { get; set; } = DateTime.UtcNow;

        // These will be calculated automatically
        public decimal MonthlyInstallment { get; set; }
        public decimal RemainingBalance { get; set; }
        public bool IsActive { get; set; } = true;
    }
}