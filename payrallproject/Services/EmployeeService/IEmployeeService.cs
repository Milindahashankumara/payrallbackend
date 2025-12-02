using payrallproject.Models.Domains;
using payrallproject.Models.Dtos;

namespace payrallproject.Services.EmployeeService
{
    public interface IEmployeeService
    {
        Task<List<Employe>> GetAllEmployeesAsync(
            string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 20);
        Task<Employe> AddEmployeAsync(EmployeDto newEmploye);
        Task<Employe?> GetEmployeByIdAsync(int id);
        Task<Employe?> UpdateEmployeAsync(int id, EmployeDto employeDto);
        Task<Employe?> DeleteEmployeAsync(int id, DateTime terminationDate);
        Task<List<Employe>> GetAllDeletedEmployesAsync(
            string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 20);
        Task<Employe?> GetDeletedEmployeByIdAsync(int id);
        Task<Employe?> RecoverDeletedEmployeAsync(int id);
        Task<List<Employe>> GetAllEmployeesHaveLeavesAsync(
            string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 1000);
        Task<Employe?> GetEmployeHaveLeavesByIdAsync(int id);
    }
}
