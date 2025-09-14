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

        public async Task<LoanRepayment> AddRepaymentAsync(int loanId, decimal paymentAmount, DateTime paymentDate)
        {
            var loan = await _dbContext.Set<Loans>()
                .Include(l => l.Repayments)
                .FirstOrDefaultAsync(l => l.Id == loanId && l.IsActive);

            if (loan == null) throw new ArgumentException("Loan not found");

            // Calculate interest and principal components
            decimal monthlyInterest = loan.RemainingBalance * (loan.InterestRate / 100 / 12);
            decimal principalPaid = paymentAmount - monthlyInterest;

            if (principalPaid < 0) throw new ArgumentException("Payment must cover at least the interest");

            var repayment = new LoanRepayment
            {
                LoanId = loanId,
                PaymentDate = paymentDate,
                InstallmentAmount = paymentAmount,
                InterestPaid = monthlyInterest,
                PrincipalPaid = principalPaid,
                RemainingBalance = loan.RemainingBalance - principalPaid,
                MonthNo = loan.Repayments.Count + 1
            };

            // Update loan remaining balance
            loan.RemainingBalance -= principalPaid;

            _dbContext.Set<LoanRepayment>().Add(repayment);
            await _dbContext.SaveChangesAsync();

            return repayment;
        }

        public async Task<List<LoanRepayment>> GetRepaymentsByLoanIdAsync(int loanId)
        {
            return await _dbContext.Set<LoanRepayment>()
                .Where(r => r.LoanId == loanId)
                .OrderBy(r => r.MonthNo)
                .ToListAsync();
        }

        public async Task<decimal> CalculateNextPaymentAmountAsync(int loanId)
        {
            var loan = await _dbContext.Set<Loans>()
                .FirstOrDefaultAsync(l => l.Id == loanId && l.IsActive);

            if (loan == null) throw new ArgumentException("Loan not found");

            return loan.MonthlyInstallment;
        }
    }
}
