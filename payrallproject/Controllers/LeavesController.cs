using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using payrallproject.Models.Domains;
using payrallproject.Models.Dtos;
using payrallproject.Services.LeavesService;

namespace payrallproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeavesController : ControllerBase
    {
        private readonly ILeavesService _leavesService;

        public LeavesController(ILeavesService leavesService)
        {
            _leavesService = leavesService;
        }

        [HttpGet("balance/{employeID}/{year}")]
        public async Task<ActionResult<LeaveBalanceDto>> GetLeaveBalance(int employeID, int year)
        {
            try
            {
                var balance = await _leavesService.GetLeaveBalanceAsync(employeID, year);
                return Ok(balance);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("balance/{employeID}")]
        public async Task<ActionResult<LeaveBalanceDto>> GetCurrentYearLeaveBalance(int employeID)
        {
            try
            {
                var currentYear = DateTime.UtcNow.Year;
                var balance = await _leavesService.GetLeaveBalanceAsync(employeID, currentYear);
                return Ok(balance);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("apply")]
        public async Task<ActionResult<Leaves>> ApplyLeave([FromBody] LeaveRequestDto leaveRequest)
        {
            try
            {
                var result = await _leavesService.ApplyLeaveAsync(leaveRequest);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("history/{employeID}")]
        public async Task<ActionResult<List<Leaves>>> GetEmployeeLeavesHistory(int employeID)
        {
            try
            {
                var history = await _leavesService.GetEmployeeLeavesHistoryAsync(employeID);
                return Ok(history);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("year/{year}")]
        public async Task<ActionResult<List<Leaves>>> GetAllLeavesForYear(int year)
        {
            try
            {
                var leaves = await _leavesService.GetAllLeavesAsync(year);
                return Ok(leaves);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("check")]
        public async Task<ActionResult<bool>> CanTakeLeave([FromBody] LeaveRequestDto leaveRequest)
        {
            try
            {
                var canTake = await _leavesService.CanTakeLeave(
                    leaveRequest.EmployeID,
                    leaveRequest.StartDate,
                    leaveRequest.EndDate,
                    leaveRequest.IsHalfDay,
                    leaveRequest.LeaveType);
                return Ok(canTake);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}