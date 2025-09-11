using payrallproject.Models.Domains;
using payrallproject.Models.Dtos;

namespace payrallproject.Services.LoanService
{
    public interface ILoanService
    {
        Task<List<Loans>> GetAllLoansAsync(
    string? filterOn = null, string? filterQuery = null,
    string? sortBy = null, bool isAscending = true,
    int pageNumber = 1, int pageSize = 10);
        Task<Loans> AddLoansAsync(LoansDto newLoans);

        Task<Loans?> GetLoansByIdAsync(int id);
        Task<Loans?> UpdateLoansAsync(int id, LoansDto loansDto);
        Task<Loans?> DeleteLoansAsync(int id);
        Task<List<Loans>> GetAllDeletedLoansAsync(
            string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 10);
        Task<Loans?> GetDeletedLoansByIdAsync(int id);
        Task<Loans?> RecoverDeletedLoansAsync(int id);
    }
}
