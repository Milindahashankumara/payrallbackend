namespace payrallproject.Models.Domains
{
    public class LoanRepayment
    {
        public int Id { get; set; }
        public int LoanId { get; set; }

        public int MonthNo { get; set; }
        public DateTime PaymentDate { get; set; }

        public decimal InstallmentAmount { get; set; }
        public decimal PrincipalPaid { get; set; }
        public decimal InterestPaid { get; set; }
        public decimal RemainingBalance { get; set; }

        public Loans Loans { get; set; }
    }
}
