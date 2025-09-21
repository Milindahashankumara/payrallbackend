using payrallproject.Models.Domains;
using payrallproject.Models.Dtos;

namespace payrallproject.Services.EmpOvertimeService
{
    public interface IEmpOvertimeService
    {
        Task<List<EmployeeOvertimeDto>> GetAllEmployeeOvertimeAsync(
            string? filterOn, string? filterQuery,
            string? sortBy, bool isAscending = true,
            int pageNumber = 1, int pageSize = 10);

        Task<EmployeeOvertimeDto?> GetEmployeeOvertimeByIdAsync(int id);
        Task<EmployeeOvertime> AddEmployeeOvertimeAsync(EmployeeOvertimeDto newOvertime);
        Task<EmployeeOvertime?> UpdateEmployeeOvertimeAsync(int id, EmployeeOvertimeDto dto);
        Task<EmployeeOvertime?> DeleteEmployeeOvertimeAsync(int id);
        Task<List<EmployeeOvertimeDto>> GetEmployeeOvertimeByEmployeeIdAsync(int employeeId,
            string? filterOn, string? filterQuery,
            string? sortBy, bool isAscending = true,
            int pageNumber = 1, int pageSize = 10);
        Task<List<EmployeeOvertimeDto>> GetEmployeeOvertimeByDateRangeAsync(DateTime startDate, DateTime endDate,
            string? filterOn, string? filterQuery,
            string? sortBy, bool isAscending = true,
            int pageNumber = 1, int pageSize = 10);
        Task<OtSumDto?> GetEmployeeOvertimeSumByIdAsync(int id, DateTime fromDate, DateTime toDate);
    }

}