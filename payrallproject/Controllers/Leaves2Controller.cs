using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using payrallproject.Models.Dtos;
using payrallproject.Services.Leaves2Service;

namespace payrallproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Leaves2Controller : ControllerBase
    {
        private readonly ILeaves2Service _leavesService;

        public Leaves2Controller(ILeaves2Service leavesService)
        {
            _leavesService = leavesService;
        }

        [HttpPost("create-leave")]
        public async Task<ActionResult<ServiceResponse<Leaves2Dto>>> CreateLeave(CreateLeaveDto createLeaveDto)
        {
            var response = await _leavesService.CreateLeaveAsync(createLeaveDto);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("employee/{employeeId}/year/{year}")]
        public async Task<ActionResult<ServiceResponse<List<Leaves2Dto>>>> GetEmployeeLeaves(int employeeId, int year)
        {
            var response = await _leavesService.GetEmployeeLeavesAsync(employeeId, year);
            return Ok(response);
        }

        [HttpGet("employee/{employeeId}/summary/{year}")]
        public async Task<ActionResult<ServiceResponse<EmployeeLeaveSummaryDto>>> GetEmployeeLeaveSummary(int employeeId, int year)
        {
            var response = await _leavesService.GetEmployeeLeaveSummaryAsync(employeeId, year);
            return Ok(response);
        }

        [HttpPost("create-nopay")]
        public async Task<ActionResult<ServiceResponse<NoPayEntryDto>>> CreateNoPayDay(CreateNoPayDto createNoPayDto)
        {
            var response = await _leavesService.CreateNoPayDayAsync(createNoPayDto);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("employee/{employeeId}/nopay-days/{year}")]
        public async Task<ActionResult<ServiceResponse<List<NoPayEntryDto>>>> GetEmployeeNoPayDays(int employeeId, int year)
        {
            var response = await _leavesService.GetEmployeeNoPayDaysAsync(employeeId, year);
            return Ok(response);
        }

        [HttpGet("employee/{employeeId}/halfdays-count/{year}")]
        public async Task<ActionResult<ServiceResponse<decimal>>> GetEmployeeHalfDaysCount(int employeeId, int year)
        {
            var response = await _leavesService.GetEmployeeHalfDaysCountAsync(employeeId, year);
            return Ok(response);
        }

        [HttpGet("employee/{employeeId}/balance/{year}")]
        public async Task<ActionResult<ServiceResponse<List<LeaveBalanceDto>>>> GetEmployeeLeaveBalance(int employeeId, int year)
        {
            var response = await _leavesService.GetEmployeeLeaveBalanceAsync(employeeId, year);
            return Ok(response);
        }

        [HttpGet("employee/{employeeId}/remaining-leaves/{year}/{leaveType}")]
        public async Task<ActionResult<ServiceResponse<decimal>>> GetRemainingLeaves(int employeeId, int year, string leaveType)
        {
            var response = await _leavesService.GetRemainingLeavesAsync(employeeId, year, leaveType);
            return Ok(response);
        }

        [HttpGet("employee/{employeeId}/eligibility")]
        public async Task<ActionResult<bool>> CheckEligibility(int employeeId)
        {
            var isEligible = await _leavesService.IsEmployeeEligibleForLeaves(employeeId);
            return Ok(isEligible);
        }
        [HttpGet("employee/{employeeId}/date-range")]
        public async Task<ActionResult<ServiceResponse<DateRangeLeaveSummaryDto>>> GetEmployeeLeavesByDateRange(
    int employeeId, [FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            var response = await _leavesService.GetEmployeeLeavesByDateRangeAsync(employeeId, fromDate, toDate);
            return Ok(response);
        }

        [HttpPost("create-leave-single")]
        public async Task<ActionResult<ServiceResponse<Leaves2Dto>>> CreateSingleLeave(CreateLeaveDto createLeaveDto)
        {
            var response = await _leavesService.CreateLeaveAsync(createLeaveDto);
            if (!response.Success)
                return BadRequest(response);
            return Ok(response);
        }

        [HttpPost("create-nopay-single")]
        public async Task<ActionResult<ServiceResponse<NoPayEntryDto>>> CreateSingleNoPayDay(CreateNoPayDto createNoPayDto)
        {
            var response = await _leavesService.CreateNoPayDayAsync(createNoPayDto);
            if (!response.Success)
                return BadRequest(response);
            return Ok(response);
        }
    }
}