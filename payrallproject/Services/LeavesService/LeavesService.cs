using Microsoft.EntityFrameworkCore;
using payrallproject.Data;
using payrallproject.Models.Domains;
using payrallproject.Models.Dtos;

namespace payrallproject.Services.LeavesService
{
    public class LeavesService : ILeavesService
    {
        private readonly AuthDbContext _dbContext;

        public LeavesService(AuthDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Leaves> GetOrCreateLeavesRecordAsync(int employeID, int year)
        {
            var leaves = await _dbContext.Leaves
                .FirstOrDefaultAsync(l => l.EmployeID == employeID && l.Year == year);

            if (leaves == null)
            {
                leaves = await CreateLeavesRecordAsync(employeID, year);
            }

            return leaves;
        }

        private async Task<Leaves> CreateLeavesRecordAsync(int employeID, int year)
        {
            var employee = await _dbContext.Employe
                .Include(e => e.EmployeeCategories)
                .FirstOrDefaultAsync(e => e.Id == employeID);

            if (employee == null)
                throw new ArgumentException("Employee not found");

            if (employee.EmployeeCategories?.DaySalarybased == true)
                throw new InvalidOperationException("Day salary based employees are not eligible for leaves");

            if (employee.JoinedDate == null)
                throw new InvalidOperationException("Employee joined date is not set");

            var joinedDate = employee.JoinedDate.Value;
            var annualLeaves = CalculateAnnualLeaves(joinedDate, year);
            var casualLeaves = 7.0; // Always 7 casual leaves

            var leaves = new Leaves
            {
                EmployeID = employeID,
                Year = year,
                AnnualLeavesAllocated = annualLeaves,
                CasualLeavesAllocated = casualLeaves,
                AnnualLeavesUsed = 0,
                CasualLeavesUsed = 0
            };

            _dbContext.Leaves.Add(leaves);
            await _dbContext.SaveChangesAsync();

            return leaves;
        }

        private double CalculateAnnualLeaves(DateTime joinedDate, int year)
        {
            var yearsOfService = year - joinedDate.Year;

            // FIRST YEAR: 0 annual leaves (only casual leaves)
            if (yearsOfService == 0)
            {
                return 0.0; // No annual leaves in first year
            }
            // SECOND YEAR: Pro-rated based on previous year's join quarter
            else if (yearsOfService == 1)
            {
                return CalculateSecondYearLeaves(joinedDate);
            }
            // THIRD YEAR ONWARDS: Always 14 annual leaves
            else
            {
                return 14.0;
            }
        }

        private double CalculateSecondYearLeaves(DateTime joinedDate)
        {
            var quarter = GetQuarter(joinedDate.Month);

            return quarter switch
            {
                1 => 14.0, // Joined in Q1 of previous year
                2 => 10.0, // Joined in Q2 of previous year
                3 => 7.0,  // Joined in Q3 of previous year
                4 => 4.0,  // Joined in Q4 of previous year
                _ => 0.0
            };
        }

        private int GetQuarter(int month)
        {
            return (month - 1) / 3 + 1;
        }

        public async Task<LeaveBalanceDto> GetLeaveBalanceAsync(int employeID, int year)
        {
            var leaves = await GetOrCreateLeavesRecordAsync(employeID, year);

            return new LeaveBalanceDto
            {
                EmployeID = employeID,
                Year = year,
                AnnualLeavesAllocated = leaves.AnnualLeavesAllocated,
                AnnualLeavesUsed = leaves.AnnualLeavesUsed,
                AnnualLeavesRemaining = leaves.AnnualLeavesRemaining,
                CasualLeavesAllocated = leaves.CasualLeavesAllocated,
                CasualLeavesUsed = leaves.CasualLeavesUsed,
                CasualLeavesRemaining = leaves.CasualLeavesRemaining,
                TotalLeavesRemaining = leaves.TotalLeavesRemaining
            };
        }

        public async Task<Leaves> ApplyLeaveAsync(LeaveRequestDto leaveRequest)
        {
            var leaveDays = await CalculateLeaveDays(leaveRequest.StartDate, leaveRequest.EndDate, leaveRequest.IsHalfDay);

            if (!await CanTakeLeave(leaveRequest.EmployeID, leaveRequest.StartDate, leaveRequest.EndDate, leaveRequest.IsHalfDay, leaveRequest.LeaveType))
                throw new InvalidOperationException("Not enough leave balance");

            var year = DateTime.UtcNow.Year;
            var leaves = await GetOrCreateLeavesRecordAsync(leaveRequest.EmployeID, year);

            if (leaveRequest.LeaveType.ToLower() == "casual")
            {
                if (leaves.CasualLeavesRemaining >= leaveDays)
                {
                    leaves.CasualLeavesUsed += leaveDays;
                }
                else
                {
                    // Use remaining casual leaves first, then use annual leaves
                    var remainingCasual = leaves.CasualLeavesRemaining;
                    leaves.CasualLeavesUsed = leaves.CasualLeavesAllocated;
                    leaves.AnnualLeavesUsed += (leaveDays - remainingCasual);
                }
            }
            else if (leaveRequest.LeaveType.ToLower() == "annual")
            {
                leaves.AnnualLeavesUsed += leaveDays;
            }

            leaves.UpdatedDate = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();

            return leaves;
        }

        public async Task<double> CalculateLeaveDays(DateTime startDate, DateTime endDate, bool isHalfDay)
        {
            if (startDate > endDate)
                throw new ArgumentException("Start date cannot be after end date");

            double totalDays = 0;
            var currentDate = startDate;

            while (currentDate <= endDate)
            {
                // Skip weekends (Saturday and Sunday)
                if (currentDate.DayOfWeek != DayOfWeek.Saturday && currentDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    totalDays += isHalfDay ? 0.5 : 1.0;
                }
                currentDate = currentDate.AddDays(1);
            }

            return totalDays;
        }

        public async Task<bool> CanTakeLeave(int employeID, DateTime startDate, DateTime endDate, bool isHalfDay, string leaveType)
        {
            var leaveDays = await CalculateLeaveDays(startDate, endDate, isHalfDay);
            var year = DateTime.UtcNow.Year;
            var leaves = await GetOrCreateLeavesRecordAsync(employeID, year);

            if (leaveType.ToLower() == "casual")
            {
                return leaves.CasualLeavesRemaining + leaves.AnnualLeavesRemaining >= leaveDays;
            }
            else if (leaveType.ToLower() == "annual")
            {
                return leaves.AnnualLeavesRemaining >= leaveDays;
            }

            return false;
        }

        public async Task<List<Leaves>> GetEmployeeLeavesHistoryAsync(int employeID)
        {
            return await _dbContext.Leaves
                .Where(l => l.EmployeID == employeID)
                .OrderByDescending(l => l.Year)
                .ToListAsync();
        }

        public async Task<List<Leaves>> GetAllLeavesAsync(int year)
        {
            return await _dbContext.Leaves
                .Include(l => l.Employe)
                .Where(l => l.Year == year)
                .ToListAsync();
        }
    }
}