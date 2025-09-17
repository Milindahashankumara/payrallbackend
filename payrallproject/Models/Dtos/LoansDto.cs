using System.ComponentModel.DataAnnotations;

namespace payrallproject.Models.Dtos
{
    public class LoansDto
    {
        public int? EmployeID { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Principal amount must be greater than zero")]
        public decimal PrincipalAmount { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Term must be at least 1 month")]
        public int TermMonths { get; set; }

        public DateTime StartDate { get; set; } = DateTime.UtcNow;

        // These should not be set by the client
        public decimal MonthlyInstallment { get; set; }
        public decimal RemainingBalance { get; set; }
        public bool IsActive { get; set; } = true;
        public bool Settled { get; set; } = false; // Changed default to false
    }
}