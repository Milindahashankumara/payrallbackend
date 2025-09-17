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

        public async Task<List<Loans>> GetAllLoansAsync(
            string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 10)
        {
            var loans = _dbContext.Loans
                .Include(l => l.Employe)
                .Where(l => l.IsActive)
                .AsQueryable();

            // Filtering
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                switch (filterOn.ToLower())
                {
                    case "employee":
                        loans = loans.Where(l => l.Employe.FullName.Contains(filterQuery) ||
                                               l.Employe.FullName.Contains(filterQuery));
                        break;
                    case "amount":
                        if (decimal.TryParse(filterQuery, out decimal amount))
                            loans = loans.Where(l => l.PrincipalAmount == amount);
                        break;
                    case "status":
                        if (bool.TryParse(filterQuery, out bool settled))
                            loans = loans.Where(l => l.Settled == settled);
                        break;
                }
            }

            // Sorting
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                switch (sortBy.ToLower())
                {
                    case "employee":
                        loans = isAscending ? loans.OrderBy(l => l.Employe.FullName) : loans.OrderByDescending(l => l.Employe.FullName);
                        break;
                    case "amount":
                        loans = isAscending ? loans.OrderBy(l => l.PrincipalAmount) : loans.OrderByDescending(l => l.PrincipalAmount);
                        break;
                    case "date":
                        loans = isAscending ? loans.OrderBy(l => l.StartDate) : loans.OrderByDescending(l => l.StartDate);
                        break;
                    default:
                        loans = isAscending ? loans.OrderBy(l => l.Id) : loans.OrderByDescending(l => l.Id);
                        break;
                }
            }

            // Pagination
            var skipResults = (pageNumber - 1) * pageSize;
            return await loans.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        public async Task<Loans> AddLoansAsync(LoansDto newLoans)
        {
            // Calculate monthly installment
            decimal monthlyInstallment = newLoans.PrincipalAmount / newLoans.TermMonths;

            var loan = new Loans
            {
                EmployeID = newLoans.EmployeID,
                PrincipalAmount = newLoans.PrincipalAmount,
                TermMonths = newLoans.TermMonths,
                MonthlyInstallment = monthlyInstallment,
                RemainingBalance = newLoans.PrincipalAmount,
                StartDate = newLoans.StartDate,
                IsActive = true,
                Settled = false
            };

            _dbContext.Loans.Add(loan);
            await _dbContext.SaveChangesAsync();

            return loan;
        }

        public async Task<Loans?> GetLoansByIdAsync(int id)
        {
            return await _dbContext.Loans
                .Include(l => l.Employe)
                .FirstOrDefaultAsync(l => l.Id == id && l.IsActive);
        }

        public async Task<Loans?> UpdateLoansAsync(int id, LoansDto loansDto)
        {
            var loan = await _dbContext.Loans.FirstOrDefaultAsync(l => l.Id == id && l.IsActive);
            if (loan == null) return null;

            loan.EmployeID = loansDto.EmployeID ?? loan.EmployeID;
            loan.PrincipalAmount = loansDto.PrincipalAmount;
            loan.TermMonths = loansDto.TermMonths;
            loan.MonthlyInstallment = loansDto.PrincipalAmount / loansDto.TermMonths;
            loan.RemainingBalance = loansDto.RemainingBalance;
            loan.StartDate = loansDto.StartDate;
            loan.Settled = loansDto.RemainingBalance <= 0;

            await _dbContext.SaveChangesAsync();
            return loan;
        }

        public async Task<Loans?> DeleteLoansAsync(int id)
        {
            var loan = await _dbContext.Loans.FirstOrDefaultAsync(l => l.Id == id && l.IsActive);
            if (loan == null) return null;

            loan.IsActive = false;
            await _dbContext.SaveChangesAsync();

            return loan;
        }

        public async Task<List<Loans>> GetAllDeletedLoansAsync(
            string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 10)
        {
            var loans = _dbContext.Loans
                .Include(l => l.Employe)
                .Where(l => !l.IsActive)
                .AsQueryable();

            // Filtering and sorting logic similar to GetAllLoansAsync
            // ... (implementation would be similar to GetAllLoansAsync)

            var skipResults = (pageNumber - 1) * pageSize;
            return await loans.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        public async Task<Loans?> GetDeletedLoansByIdAsync(int id)
        {
            return await _dbContext.Loans
                .Include(l => l.Employe)
                .FirstOrDefaultAsync(l => l.Id == id && !l.IsActive);
        }

        public async Task<Loans?> RecoverDeletedLoansAsync(int id)
        {
            var loan = await _dbContext.Loans.FirstOrDefaultAsync(l => l.Id == id && !l.IsActive);
            if (loan == null) return null;

            loan.IsActive = true;
            await _dbContext.SaveChangesAsync();

            return loan;
        }
    }
}