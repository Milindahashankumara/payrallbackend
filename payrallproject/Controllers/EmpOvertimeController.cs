using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using payrallproject.Models.Domains;
using payrallproject.Models.Dtos;
using payrallproject.Services.EmpOvertimeService;

namespace payrallproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpOvertimeController : ControllerBase
    {
        private readonly IEmpOvertimeService _empOvertimeService;

        public EmpOvertimeController(IEmpOvertimeService empOvertimeService)
        {
            _empOvertimeService = empOvertimeService;
        }

        [HttpGet("sum/{id}")]
        public async Task<ActionResult<OtSumDto>> GetOvertimeSum(int id,[FromQuery] DateTime fromDate,[FromQuery] DateTime toDate)
        {
            var result = await _empOvertimeService.GetEmployeeOvertimeSumByIdAsync(id, fromDate, toDate);

            if (result == null)
                return NotFound(new { message = $"Employee with ID {id} not found or no records in the given date range." });

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<EmployeeOvertimeDto>>> GetAll(
            string? filterOn, string? filterQuery,
            string? sortBy, bool isAscending = true,
            int pageNumber = 1, int pageSize = 10)
        {
            var overtimeRecords = await _empOvertimeService.GetAllEmployeeOvertimeAsync(filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);
            return Ok(overtimeRecords);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeOvertimeDto>> GetById(int id)
        {
            var overtime = await _empOvertimeService.GetEmployeeOvertimeByIdAsync(id);
            if (overtime == null) return NotFound();
            return Ok(overtime);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeOvertime>> Create(EmployeeOvertimeDto dto)
        {
            var overtime = await _empOvertimeService.AddEmployeeOvertimeAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = overtime.Id }, overtime);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EmployeeOvertime>> Update(int id, EmployeeOvertimeDto dto)
        {
            var updated = await _empOvertimeService.UpdateEmployeeOvertimeAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<EmployeeOvertime>> Delete(int id)
        {
            var deleted = await _empOvertimeService.DeleteEmployeeOvertimeAsync(id);
            if (deleted == null) return NotFound();
            return Ok(deleted);
        }

        [HttpGet("employee/{employeeId}")]
        public async Task<ActionResult<List<EmployeeOvertimeDto>>> GetByEmployeeId(int employeeId,
            string? filterOn, string? filterQuery,
            string? sortBy, bool isAscending = true,
            int pageNumber = 1, int pageSize = 10)
        {
            var overtimeRecords = await _empOvertimeService.GetEmployeeOvertimeByEmployeeIdAsync(employeeId, filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);
            return Ok(overtimeRecords);
        }

        [HttpGet("date-range")]
        public async Task<ActionResult<List<EmployeeOvertimeDto>>> GetByDateRange(
            DateTime startDate, DateTime endDate,
            string? filterOn, string? filterQuery,
            string? sortBy, bool isAscending = true,
            int pageNumber = 1, int pageSize = 10)
        {
            var overtimeRecords = await _empOvertimeService.GetEmployeeOvertimeByDateRangeAsync(startDate, endDate, filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);
            return Ok(overtimeRecords);
        }
    }
}