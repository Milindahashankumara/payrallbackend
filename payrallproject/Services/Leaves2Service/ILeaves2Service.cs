using payrallproject.Models.Dtos;

namespace payrallproject.Services.Leaves2Service
{
    public interface ILeaves2Service
    {
        Task<ServiceResponse<Leaves2Dto>> CreateLeaveAsync(CreateLeaveDto createLeaveDto);
        Task<ServiceResponse<List<Leaves2Dto>>> GetEmployeeLeavesAsync(int employeeId, int year);
        Task<ServiceResponse<EmployeeLeaveSummaryDto>> GetEmployeeLeaveSummaryAsync(int employeeId, int year);
        Task<ServiceResponse<NoPayEntryDto>> CreateNoPayDayAsync(CreateNoPayDto createNoPayDto);
        Task<ServiceResponse<List<NoPayEntryDto>>> GetEmployeeNoPayDaysAsync(int employeeId, int year);
        Task<ServiceResponse<decimal>> GetEmployeeHalfDaysCountAsync(int employeeId, int year);
        Task<ServiceResponse<List<LeaveBalanceDto>>> GetEmployeeLeaveBalanceAsync(int employeeId, int year);
        Task<ServiceResponse<decimal>> GetRemainingLeavesAsync(int employeeId, int year, string leaveType);
        Task<bool> IsEmployeeEligibleForLeaves(int employeeId);
    }

    public class ServiceResponse<T>
    {
        public T? Data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
    }
}