using payrallproject.Models.Domains;

namespace payrallproject.Services.LoanRepaymentService
{
    public interface ILoanRepaymentService
    {
        Task<LoanRepayment> AddRepaymentAsync(int loanId, decimal paymentAmount, DateTime paymentDate, string description);
        Task<List<LoanRepayment>> GetRepaymentsByLoanIdAsync(int loanId);
        Task<decimal> CalculateNextPaymentAmountAsync(int loanId);
    }
}
