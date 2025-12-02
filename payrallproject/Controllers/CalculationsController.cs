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
            var report = await _salaryReportService.GenerateAndStoreSalaryReportAsync(dto);
            return Ok(report);
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
