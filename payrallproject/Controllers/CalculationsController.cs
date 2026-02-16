using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using payrallproject.Models.Domains;
using payrallproject.Models.Dtos;
using payrallproject.Services.SalaryReportService;

namespace payrallproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculationsController : ControllerBase
    {
        private readonly ISalaryReportService _salaryReportService;

        public CalculationsController(ISalaryReportService salaryReportService)
        {
            _salaryReportService = salaryReportService;
        }

        [HttpPost("generate")]
        public async Task<IActionResult> GenerateSalaryReport([FromBody] SalaryReportDto dto)
        {
            try
            {
                // Debug logging - Input
                Console.WriteLine($"[DEBUG INPUT] ====================================");
                Console.WriteLine($"[DEBUG INPUT] EmployeeId: {dto.EmployeeId}");
                Console.WriteLine($"[DEBUG INPUT] DaySalary: {dto.DaySalary}");
                Console.WriteLine($"[DEBUG INPUT] KpiRate (Casual): {dto.KpiRate}");
                Console.WriteLine($"[DEBUG INPUT] KpiAmount (Staff): {dto.KpiAmount}");
                Console.WriteLine($"[DEBUG INPUT] WorkingDays: {dto.WorkingDays}");
                Console.WriteLine($"[DEBUG INPUT] Incentives: {dto.Incentives}");
                Console.WriteLine($"[DEBUG INPUT] ===================================="); ;

                var report = await _salaryReportService.GenerateAndStoreSalaryReportAsync(dto);

                // Debug logging - Output
                Console.WriteLine($"[DEBUG OUTPUT] ====================================");
                Console.WriteLine($"[DEBUG OUTPUT] Report ID: {report.Id}");
                Console.WriteLine($"[DEBUG OUTPUT] IsDaySalaryBased: {report.IsDaySalaryBased}");
                Console.WriteLine($"[DEBUG OUTPUT] DaySalary: {report.DaySalary}");
                Console.WriteLine($"[DEBUG OUTPUT] KpiRate: {report.KpiRate}");
                Console.WriteLine($"[DEBUG OUTPUT] KpiAllowance saved: {report.KpiAllowance}");
                Console.WriteLine($"[DEBUG OUTPUT] Wages: {report.Wages}");
                Console.WriteLine($"[DEBUG OUTPUT] Incentives calculated: {report.Incentives}");
                Console.WriteLine($"[DEBUG OUTPUT] GrossSalary: {report.GrossSalary}");
                Console.WriteLine($"[DEBUG OUTPUT] ===================================="); ;

                return Ok(report);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
                Console.WriteLine($"STACK: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"INNER: {ex.InnerException.Message}");
                }
                return StatusCode(500, new { message = ex.Message, details = ex.InnerException?.Message, stackTrace = ex.StackTrace });
            }
        }

        [HttpGet("salaryreports")]
        public async Task<ActionResult<List<SalaryReport>>> GetAllSalaryReports()
        {
            var reports = await _salaryReportService.GetAllSalaryReportsAsync();
            return Ok(reports);
        }

        [HttpGet("salaryreports/employee/{employeeId}")]
        public async Task<ActionResult<List<SalaryReport>>> GetAllSalaryReportsByEmployeeId(int employeeId)
        {
            var reports = await _salaryReportService.GetAllSalaryReportsByEmployeeIdAsync(employeeId);
            return Ok(reports);
        }

        [HttpPut("salaryreports/{id}")]
        public async Task<ActionResult<SalaryReport>> UpdateSalaryReport(int id, [FromBody] SalaryReportDto dto)
        {
            var updatedReport = await _salaryReportService.UpdateSalaryReportAsync(id, dto);
            if (updatedReport == null) return NotFound();
            return Ok(updatedReport);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalaryReport(int id)
        {
            var result = await _salaryReportService.DeleteSalaryReportAsync(id);

            if (!result)
                return NotFound($"Salary report with ID {id} not found");

            return Ok($"Salary report with ID {id} deleted successfully");
        }
    }
}
