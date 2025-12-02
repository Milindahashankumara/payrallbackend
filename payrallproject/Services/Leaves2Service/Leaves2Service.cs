using Microsoft.EntityFrameworkCore;
using payrallproject.Data;
using payrallproject.Models.Domains;
using payrallproject.Models.Dtos;

namespace payrallproject.Services.Leaves2Service
{
    public class Leaves2Service : ILeaves2Service
    {
        private readonly AuthDbContext _context;

        public Leaves2Service(AuthDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<Leaves2Dto>> CreateLeaveAsync(CreateLeaveDto createLeaveDto)
        {
            var response = new ServiceResponse<Leaves2Dto>();

            try
            {
                // Check if employee is eligible for leaves
                if (!await IsEmployeeEligibleForLeaves(createLeaveDto.EmployeeId))
                {
                    response.Success = false;
                    response.Message = "Employee is not eligible for leaves (Day salary based employees don't qualify)";
                    return response;
                }

                var employee = await _context.Employe
                    .Include(e => e.EmployeeCategories)
                    .FirstOrDefaultAsync(e => e.Id == createLeaveDto.EmployeeId);

                if (employee == null)
                {
                    response.Success = false;
                    response.Message = "Employee not found";
                    return response;
                }

                // Calculate number of days
                var numberOfDays = CalculateNumberOfDays(createLeaveDto.StartDate, createLeaveDto.EndDate, createLeaveDto.IsHalfDay);

                var year = createLeaveDto.StartDate.Year;

                // Check leave balance
                var leaveBalance = await GetOrCreateLeaveBalanceAsync(createLeaveDto.EmployeeId, createLeaveDto.LeaveType, year);
                if (leaveBalance.BalanceDays < numberOfDays)
                {
                    response.Success = false;
                    response.Message = $"Insufficient {createLeaveDto.LeaveType} leave balance. Available: {leaveBalance.BalanceDays}, Requested: {numberOfDays}";
                    return response;
                }

                var leave = new Leaves2
                {
                    EmployeeId = createLeaveDto.EmployeeId,
                    StartDate = createLeaveDto.StartDate,
                    EndDate = createLeaveDto.EndDate,
                    IsHalfDay = createLeaveDto.IsHalfDay,
                    IsFirstHalfDay = createLeaveDto.IsFirstHalfDay,
                    NumberOfDays = numberOfDays,
                    LeaveType = createLeaveDto.LeaveType,
                    Reason = createLeaveDto.Reason,
                    Status = "Approved",
                    Year = year,
                    CreatedDate = DateTime.UtcNow
                };

                _context.Leaves2.Add(leave);

                // Update leave balance
                leaveBalance.UsedDays += numberOfDays;
                leaveBalance.BalanceDays = leaveBalance.EntitledDays - leaveBalance.UsedDays;
                leaveBalance.UpdatedDate = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                response.Data = MapToLeaves2Dto(leave);
                response.Message = "Leave created successfully";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error creating leave: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse<List<Leaves2Dto>>> GetEmployeeLeavesAsync(int employeeId, int year)
        {
            var response = new ServiceResponse<List<Leaves2Dto>>();

            try
            {
                var leaves = await _context.Leaves2
                    .Where(l => l.EmployeeId == employeeId && l.Year == year)
                    .OrderByDescending(l => l.StartDate)
                    .ToListAsync();

                response.Data = leaves.Select(MapToLeaves2Dto).ToList();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error retrieving leaves: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse<EmployeeLeaveSummaryDto>> GetEmployeeLeaveSummaryAsync(int employeeId, int year)
        {
            var response = new ServiceResponse<EmployeeLeaveSummaryDto>();

            try
            {
                var employee = await _context.Employe
                    .Include(e => e.EmployeeCategories)
                    .FirstOrDefaultAsync(e => e.Id == employeeId);

                if (employee == null)
                {
                    response.Success = false;
                    response.Message = "Employee not found";
                    return response;
                }

                // Get leave balances
                var leaveBalances = await GetEmployeeLeaveBalanceAsync(employeeId, year);

                // Get leaves
                var leavesResponse = await GetEmployeeLeavesAsync(employeeId, year);

                // Get no-pay days
                var noPayResponse = await GetEmployeeNoPayDaysAsync(employeeId, year);

                // Get half days count
                var halfDaysResponse = await GetEmployeeHalfDaysCountAsync(employeeId, year);

                var summary = new EmployeeLeaveSummaryDto
                {
                    EmployeeId = employeeId,
                    EmployeeName = employee.FullName ?? "Unknown",
                    EmployeeNumber = employee.EmployeeNumber ?? "Unknown",
                    Year = year,
                    LeaveBalances = leaveBalances.Data ?? new List<LeaveBalanceDto>(),
                    TotalHalfDays = halfDaysResponse.Data,
                    Leaves = leavesResponse.Data ?? new List<Leaves2Dto>(),
                    NoPayDays = noPayResponse.Data ?? new List<NoPayEntryDto>()
                };

                response.Data = summary;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error retrieving leave summary: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse<NoPayEntryDto>> CreateNoPayDayAsync(CreateNoPayDto createNoPayDto)
        {
            var response = new ServiceResponse<NoPayEntryDto>();

            try
            {
                var employee = await _context.Employe.FindAsync(createNoPayDto.EmployeeId);
                if (employee == null)
                {
                    response.Success = false;
                    response.Message = "Employee not found";
                    return response;
                }

                var noPayEntry = new NoPayEntry
                {
                    EmployeeId = createNoPayDto.EmployeeId,
                    NoPayDate = createNoPayDto.NoPayDate,
                    Reason = createNoPayDto.Reason,
                    CreatedDate = DateTime.UtcNow
                };

                _context.NoPayEntries.Add(noPayEntry);
                await _context.SaveChangesAsync();

                response.Data = new NoPayEntryDto
                {
                    Id = noPayEntry.Id,
                    EmployeeId = noPayEntry.EmployeeId,
                    NoPayDate = noPayEntry.NoPayDate,
                    Reason = noPayEntry.Reason
                };
                response.Message = "No-pay day created successfully";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error creating no-pay day: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse<List<NoPayEntryDto>>> GetEmployeeNoPayDaysAsync(int employeeId, int year)
        {
            var response = new ServiceResponse<List<NoPayEntryDto>>();

            try
            {
                var noPayDays = await _context.NoPayEntries
                    .Where(n => n.EmployeeId == employeeId && n.NoPayDate.Year == year)
                    .OrderByDescending(n => n.NoPayDate)
                    .ToListAsync();

                response.Data = noPayDays.Select(n => new NoPayEntryDto
                {
                    Id = n.Id,
                    EmployeeId = n.EmployeeId,
                    NoPayDate = n.NoPayDate,
                    Reason = n.Reason
                }).ToList();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error retrieving no-pay days: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse<decimal>> GetEmployeeHalfDaysCountAsync(int employeeId, int year)
        {
            var response = new ServiceResponse<decimal>();

            try
            {
                var halfDays = await _context.Leaves2
                    .Where(l => l.EmployeeId == employeeId &&
                               l.Year == year &&
                               l.Status == "Approved" &&
                               l.IsHalfDay)
                    .SumAsync(l => 0.5m);

                response.Data = halfDays;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error calculating half days: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse<List<LeaveBalanceDto>>> GetEmployeeLeaveBalanceAsync(int employeeId, int year)
        {
            var response = new ServiceResponse<List<LeaveBalanceDto>>();

            try
            {
                // Ensure leave balances exist for this year
                await InitializeLeaveBalancesAsync(employeeId, year);

                var balances = await _context.LeaveBalances
                    .Where(lb => lb.EmployeeId == employeeId && lb.Year == year)
                    .ToListAsync();

                response.Data = balances.Select(b => new LeaveBalanceDto
                {
                    LeaveType = b.LeaveType,
                    EntitledDays = b.EntitledDays,
                    UsedDays = b.UsedDays,
                    BalanceDays = b.BalanceDays
                }).ToList();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error retrieving leave balance: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse<decimal>> GetRemainingLeavesAsync(int employeeId, int year, string leaveType)
        {
            var response = new ServiceResponse<decimal>();

            try
            {
                var balance = await _context.LeaveBalances
                    .FirstOrDefaultAsync(lb => lb.EmployeeId == employeeId &&
                                             lb.Year == year &&
                                             lb.LeaveType == leaveType);

                response.Data = balance?.BalanceDays ?? 0;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error retrieving remaining leaves: {ex.Message}";
            }

            return response;
        }

        public async Task<bool> IsEmployeeEligibleForLeaves(int employeeId)
        {
            var employee = await _context.Employe
                .Include(e => e.EmployeeCategories)
                .FirstOrDefaultAsync(e => e.Id == employeeId);

            return employee?.EmployeeCategories?.DaySalarybased == false;
        }

        // Helper Methods
        private decimal CalculateNumberOfDays(DateTime startDate, DateTime endDate, bool isHalfDay)
        {
            if (isHalfDay) return 0.5m;

            var totalDays = (endDate - startDate).Days + 1;
            return totalDays;
        }

        private async Task<LeaveBalance> GetOrCreateLeaveBalanceAsync(int employeeId, string leaveType, int year)
        {
            var balance = await _context.LeaveBalances
                .FirstOrDefaultAsync(lb => lb.EmployeeId == employeeId &&
                                         lb.LeaveType == leaveType &&
                                         lb.Year == year);

            if (balance == null)
            {
                await InitializeLeaveBalancesAsync(employeeId, year);
                balance = await _context.LeaveBalances
                    .FirstOrDefaultAsync(lb => lb.EmployeeId == employeeId &&
                                             lb.LeaveType == leaveType &&
                                             lb.Year == year);
            }

            return balance!;
        }

        private async Task InitializeLeaveBalancesAsync(int employeeId, int year)
        {
            var employee = await _context.Employe
                .Include(e => e.EmployeeCategories)
                .FirstOrDefaultAsync(e => e.Id == employeeId);

            if (employee == null || employee.JoinedDate == null) return;

            // Check if balances already exist for this year
            var existingBalances = await _context.LeaveBalances
                .AnyAsync(lb => lb.EmployeeId == employeeId && lb.Year == year);

            if (!existingBalances)
            {
                var (annualLeaves, casualLeaves) = CalculateLeaveEntitlements(employee.JoinedDate.Value, year);

                var annualBalance = new LeaveBalance
                {
                    EmployeeId = employeeId,
                    LeaveType = "Annual",
                    Year = year,
                    EntitledDays = annualLeaves,
                    UsedDays = 0,
                    BalanceDays = annualLeaves,
                    CreatedDate = DateTime.UtcNow
                };

                var casualBalance = new LeaveBalance
                {
                    EmployeeId = employeeId,
                    LeaveType = "Casual",
                    Year = year,
                    EntitledDays = casualLeaves,
                    UsedDays = 0,
                    BalanceDays = casualLeaves,
                    CreatedDate = DateTime.UtcNow
                };

                _context.LeaveBalances.AddRange(annualBalance, casualBalance);
                await _context.SaveChangesAsync();
            }
        }

        private (int annualLeaves, int casualLeaves) CalculateLeaveEntitlements(DateTime joinDate, int year)
        {
            var yearsOfWork = year - joinDate.Year;
            var joinQuarter = (joinDate.Month - 1) / 3 + 1;

            if (yearsOfWork == 0) // First year
            {
                return (0, 7);
            }
            else if (yearsOfWork == 1) // Second year
            {
                var annualLeaves = joinQuarter switch
                {
                    1 => 14,
                    2 => 10,
                    3 => 7,
                    4 => 4,
                    _ => 0
                };
                return (annualLeaves, 7);
            }
            else // Third year onwards
            {
                return (14, 7);
            }
        }

        private Leaves2Dto MapToLeaves2Dto(Leaves2 leave)
        {
            return new Leaves2Dto
            {
                Id = leave.Id,
                EmployeeId = leave.EmployeeId,
                StartDate = leave.StartDate,
                EndDate = leave.EndDate,
                IsHalfDay = leave.IsHalfDay,
                IsFirstHalfDay = leave.IsFirstHalfDay,
                NumberOfDays = leave.NumberOfDays,
                LeaveType = leave.LeaveType,
                Reason = leave.Reason,
                Status = leave.Status,
                Year = leave.Year
            };
        }

        public async Task<ServiceResponse<DateRangeLeaveSummaryDto>> GetEmployeeLeavesByDateRangeAsync(int employeeId, DateTime fromDate, DateTime toDate)
        {
            var response = new ServiceResponse<DateRangeLeaveSummaryDto>();

            try
            {
                // Get approved leaves within date range
                var leaves = await _context.Leaves2
                    .Where(l => l.EmployeeId == employeeId &&
                               l.Status == "Approved" &&
                               l.StartDate >= fromDate &&
                               l.EndDate <= toDate)
                    .ToListAsync();

                // Calculate full leave days (excluding half days)
                var fullLeaveDays = leaves
                    .Where(l => !l.IsHalfDay)
                    .Sum(l => (double)l.NumberOfDays);

                // Calculate half days count
                var halfDaysCount = leaves
                    .Where(l => l.IsHalfDay)
                    .Count() * 0.5;

                // Get no-pay days within date range
                var noPayDays = await _context.NoPayEntries
                    .Where(n => n.EmployeeId == employeeId &&
                               n.NoPayDate >= fromDate &&
                               n.NoPayDate <= toDate)
                    .CountAsync();

                var summary = new DateRangeLeaveSummaryDto
                {
                    EmployeeId = employeeId,
                    FromDate = fromDate,
                    ToDate = toDate,
                    LeaveDays = (decimal)fullLeaveDays,
                    HalfDays = (decimal)halfDaysCount,
                    NoPayDays = noPayDays
                };

                response.Data = summary;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error retrieving date range summary: {ex.Message}";
            }

            return response;
        }
    }
}