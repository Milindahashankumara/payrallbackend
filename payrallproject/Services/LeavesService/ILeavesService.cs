using payrallproject.Models.Domains;
using payrallproject.Models.Dtos;

namespace payrallproject.Services.LeavesService
{
    public interface ILeavesService
    {
        Task<Leaves> GetOrCreateLeavesRecordAsync(int employeID, int year);
        Task<LeaveBalanceDto> GetLeaveBalanceAsync(int employeID, int year);
        Task<Leaves> ApplyLeaveAsync(LeaveRequestDto leaveRequest);
        Task<List<Leaves>> GetEmployeeLeavesHistoryAsync(int employeID);
        Task<List<Leaves>> GetAllLeavesAsync(int year);
        Task<bool> CanTakeLeave(int employeID, DateTime startDate, DateTime endDate, bool isHalfDay, string leaveType);
        Task<double> CalculateLeaveDays(DateTime startDate, DateTime endDate, bool isHalfDay);
    }
}