using Microsoft.EntityFrameworkCore;
using payrallproject.Data;
using payrallproject.Models.Domains;

namespace payrallproject.Services.LoanRepaymentService
{
    public class LoanRepaymentService : ILoanRepaymentService
    {
        private readonly AuthDbContext _dbContext;

        public LoanRepaymentService(AuthDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<LoanRepayment> AddRepaymentAsync(int loanId, decimal paymentAmount, DateTime paymentDate, string description)
        {
            var loan = await _dbContext.Loans.FirstOrDefaultAsync(l => l.Id == loanId && l.IsActive);
            if (loan == null)
                throw new ArgumentException("Loan not found or inactive");

            if (paymentAmount <= 0)
                throw new ArgumentException("Payment amount must be greater than zero");

            if (paymentAmount > loan.RemainingBalance)
                throw new ArgumentException("Payment amount cannot exceed remaining balance");

            // Calculate month number based on start date
            var monthsSinceStart = ((paymentDate.Year - loan.StartDate.Year) * 12) + paymentDate.Month - loan.StartDate.Month;
            int monthNo = Math.Max(1, monthsSinceStart + 1);

            // Update loan remaining balance
            loan.RemainingBalance -= paymentAmount;
            loan.Settled = loan.RemainingBalance <= 0;

            var repayment = new LoanRepayment
            {
                LoanId = loanId,
                MonthNo = monthNo,
                PaymentDate = paymentDate,
                InstallmentAmount = paymentAmount,
                RemainingBalance = loan.RemainingBalance,
                Description = description
            };

            _dbContext.Loanrepayment.Add(repayment);
            await _dbContext.SaveChangesAsync();

            return repayment;
        }

        public async Task<List<LoanRepayment>> GetRepaymentsByLoanIdAsync(int loanId)
        {
            return await _dbContext.Loanrepayment
                .Where(r => r.LoanId == loanId)
                .OrderBy(r => r.MonthNo)
                .ToListAsync();
        }

        public async Task<decimal> CalculateNextPaymentAmountAsync(int loanId)
        {
            var loan = await _dbContext.Loans.FirstOrDefaultAsync(l => l.Id == loanId && l.IsActive);
            if (loan == null) return 0;

            // Return the minimum of monthly installment or remaining balance
            return Math.Min(loan.MonthlyInstallment, loan.RemainingBalance);
        }
    }
}