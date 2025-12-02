using System.ComponentModel.DataAnnotations.Schema;

namespace payrallproject.Models.Domains
{
    public class LoanRepayment
    {
        public int Id { get; set; }
        public int? LoanId { get; set; }
        [ForeignKey(nameof(LoanId))]
        public Loans Loans { get; set; }
        public int MonthNo { get; set; }
        public DateTime PaymentDate { get; set; }

        public decimal InstallmentAmount { get; set; }
        public decimal RemainingBalance { get; set; }
        public string Description { get; set; }
    }
}
