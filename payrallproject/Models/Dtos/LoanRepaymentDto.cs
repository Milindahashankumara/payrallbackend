using System.ComponentModel.DataAnnotations;

namespace payrallproject.Models.Dtos
{
    public class LoanRepaymentDto
    {
        public int? LoanId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Installment amount must be greater than zero")]
        public decimal InstallmentAmount { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }

        public string Description { get; set; }

        // These should not be set by the client
        public int MonthNo { get; set; }
        public decimal RemainingBalance { get; set; }
    }
}