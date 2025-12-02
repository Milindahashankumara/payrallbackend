using payrallproject.Models.Domains;
using payrallproject.Models.Dtos;

namespace payrallproject.Services.SalaryReportService
{
    public interface ISalaryReportService
    {
        Task<SalaryReport> GenerateAndStoreSalaryReportAsync(SalaryReportDto dto);
        Task<List<SalaryReport>> GetAllSalaryReportsAsync();
        Task<List<SalaryReport>> GetAllSalaryReportsByEmployeeIdAsync(int employeeId);
        Task<SalaryReport?> UpdateSalaryReportAsync(int id, SalaryReportDto dto);
        Task<bool> DeleteSalaryReportAsync(int id);
    }
}
