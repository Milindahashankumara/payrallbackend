using Microsoft.EntityFrameworkCore;
using payrallproject.Data;
using payrallproject.Models.Domains;
using payrallproject.Models.Dtos;

namespace payrallproject.Services.LoanService
{
    public class LoanService : ILoanService
    {
        private readonly AuthDbContext _dbContext;

        public LoanService(AuthDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Loans> AddLoansAsync(LoansDto newLoans)
        {
            var loan = new Loans
            {
                EmployeID = newLoans.EmployeID,
                PrincipalAmount = newLoans.PrincipalAmount,
                InterestRate = newLoans.InterestRate,
                TermMonths = newLoans.TermMonths,
                LoanType = newLoans.LoanType,
                MonthlyInstallment = newLoans.MonthlyInstallment,
                RemainingBalance = newLoans.RemainingBalance,
                StartDate = newLoans.StartDate,
                IsActive = newLoans.IsActive
            };

            _dbContext.Set<Loans>().Add(loan);
            await _dbContext.SaveChangesAsync();
            return loan;
        }

        public async Task<Loans?> GetLoansByIdAsync(int id)
        {
            return await _dbContext.Set<Loans>()
                .Include(l => l.Employe)
                .Include(l => l.Repayments)
                .FirstOrDefaultAsync(l => l.Id == id && l.IsActive);
        }

        public async Task<Loans?> UpdateLoansAsync(int id, LoansDto loansDto)
        {
            var loan = await _dbContext.Set<Loans>().FirstOrDefaultAsync(l => l.Id == id && l.IsActive);
            if (loan == null) return null;

            loan.EmployeID = loansDto.EmployeID;
            loan.PrincipalAmount = loansDto.PrincipalAmount;
            loan.InterestRate = loansDto.InterestRate;
            loan.TermMonths = loansDto.TermMonths;
            loan.LoanType = loansDto.LoanType;
            loan.MonthlyInstallment = loansDto.MonthlyInstallment;
            loan.RemainingBalance = loansDto.RemainingBalance;
            loan.StartDate = loansDto.StartDate;
            loan.IsActive = loansDto.IsActive;

            await _dbContext.SaveChangesAsync();
            return loan;
        }

        public async Task<Loans?> DeleteLoansAsync(int id)
        {
            var loan = await _dbContext.Set<Loans>().FirstOrDefaultAsync(l => l.Id == id && l.IsActive);
            if (loan == null) return null;

            loan.IsActive = false;
            await _dbContext.SaveChangesAsync();
            return loan;
        }

        public async Task<List<Loans>> GetAllLoansAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 10)
        {
            var query = _dbContext.Set<Loans>().Where(l => l.IsActive);

            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                if (filterOn.Equals("EmployeID", StringComparison.OrdinalIgnoreCase))
                    query = query.Where(l => l.EmployeID.ToString().Contains(filterQuery));
                else if (filterOn.Equals("PrincipalAmount", StringComparison.OrdinalIgnoreCase))
                    query = query.Where(l => l.PrincipalAmount.ToString().Contains(filterQuery));
                // Add more filters as needed
            }

            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                query = sortBy switch
                {
                    "PrincipalAmount" => isAscending ? query.OrderBy(l => l.PrincipalAmount) : query.OrderByDescending(l => l.PrincipalAmount),
                    "StartDate" => isAscending ? query.OrderBy(l => l.StartDate) : query.OrderByDescending(l => l.StartDate),
                    _ => query
                };
            }

            return await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Include(l => l.Employe)
                .Include(l => l.Repayments)
                .ToListAsync();
        }

        public async Task<List<Loans>> GetAllDeletedLoansAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 10)
        {
            var query = _dbContext.Set<Loans>().Where(l => !l.IsActive);

            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                if (filterOn.Equals("EmployeID", StringComparison.OrdinalIgnoreCase))
                    query = query.Where(l => l.EmployeID.ToString().Contains(filterQuery));
                else if (filterOn.Equals("PrincipalAmount", StringComparison.OrdinalIgnoreCase))
                    query = query.Where(l => l.PrincipalAmount.ToString().Contains(filterQuery));
                // Add more filters as needed
            }

            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                query = sortBy switch
                {
                    "PrincipalAmount" => isAscending ? query.OrderBy(l => l.PrincipalAmount) : query.OrderByDescending(l => l.PrincipalAmount),
                    "StartDate" => isAscending ? query.OrderBy(l => l.StartDate) : query.OrderByDescending(l => l.StartDate),
                    _ => query
                };
            }

            return await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Include(l => l.Employe)
                .Include(l => l.Repayments)
                .ToListAsync();
        }

        public async Task<Loans?> GetDeletedLoansByIdAsync(int id)
        {
            return await _dbContext.Set<Loans>()
                .Include(l => l.Employe)
                .Include(l => l.Repayments)
                .FirstOrDefaultAsync(l => l.Id == id && !l.IsActive);
        }

        public async Task<Loans?> RecoverDeletedLoansAsync(int id)
        {
            var loan = await _dbContext.Set<Loans>().FirstOrDefaultAsync(l => l.Id == id && !l.IsActive);
            if (loan == null) return null;

            loan.IsActive = true;
            await _dbContext.SaveChangesAsync();
            return loan;
        }
    }
}
