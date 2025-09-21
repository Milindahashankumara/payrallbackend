using Microsoft.EntityFrameworkCore;
using payrallproject.Data;
using payrallproject.Models.Domains;
using payrallproject.Models.Dtos;

namespace payrallproject.Services.EmpOvertimeService
{
    public class EmpOvertimeService : IEmpOvertimeService
    {
        private readonly AuthDbContext _dbContext;

        public EmpOvertimeService(AuthDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<EmployeeOvertimeDto>> GetAllEmployeeOvertimeAsync(
            string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 10)
        {
            var query = _dbContext.EmployeeOvertimes
                .Include(eo => eo.Employe)
                .Include(eo => eo.OT)
                .AsQueryable();

            // Filtering
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                if (filterOn.Equals("EmployeeName", StringComparison.OrdinalIgnoreCase))
                {
                    query = query.Where(eo => eo.Employe != null &&
                        (eo.Employe.FullName).Contains(filterQuery));
                }
                else if (filterOn.Equals("OTType", StringComparison.OrdinalIgnoreCase))
                {
                    query = query.Where(eo => eo.OT != null && eo.OT.Name!.Contains(filterQuery));
                }
                else if (filterOn.Equals("HoursWorked", StringComparison.OrdinalIgnoreCase))
                {
                    if (int.TryParse(filterQuery, out int hours))
                    {
                        query = query.Where(eo => eo.HoursWorked == hours);
                    }
                }
            }

            // Sorting
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                query = sortBy.ToLower() switch
                {
                    "dateworked" => isAscending
                        ? query.OrderBy(eo => eo.DateWorked)
                        : query.OrderByDescending(eo => eo.DateWorked),
                    "hoursworked" => isAscending
                        ? query.OrderBy(eo => eo.HoursWorked)
                        : query.OrderByDescending(eo => eo.HoursWorked),
                    "employeename" => isAscending
                        ? query.OrderBy(eo => eo.Employe != null ? eo.Employe.FullName : "")
                        : query.OrderByDescending(eo => eo.Employe != null ? eo.Employe.FullName : ""),
                    "ottype" => isAscending
                        ? query.OrderBy(eo => eo.OT != null ? eo.OT.Name : "")
                        : query.OrderByDescending(eo => eo.OT != null ? eo.OT.Name : ""),
                    _ => query.OrderBy(eo => eo.Id)
                };
            }

            // Pagination
            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            // Projection into DTO
            return await query.Select(eo => new EmployeeOvertimeDto
            {
                Id = eo.Id,
                EmployeId = eo.EmployeId,
                OtId = eo.OtId,
                DateWorked = eo.DateWorked,
                HoursWorked = eo.HoursWorked,
                EmployeeName = eo.Employe != null ? eo.Employe.FullName : null,
                OTType = eo.OT != null ? eo.OT.Name : null
            }).ToListAsync();
        }

        public async Task<EmployeeOvertimeDto?> GetEmployeeOvertimeByIdAsync(int id)
        {
            return await _dbContext.EmployeeOvertimes
                .Include(eo => eo.Employe)
                .Include(eo => eo.OT)
                .Where(eo => eo.Id == id)
                .Select(eo => new EmployeeOvertimeDto
                {
                    Id = eo.Id,
                    EmployeId = eo.EmployeId,
                    OtId = eo.OtId,
                    DateWorked = eo.DateWorked,
                    HoursWorked = eo.HoursWorked,
                    Remarks = eo.Remarks,
                    EmployeeName = eo.Employe != null ? eo.Employe.FullName : null,
                    OTType = eo.OT != null ? eo.OT.Name : null
                })
                .FirstOrDefaultAsync();
        }
        public async Task<OtSumDto?> GetEmployeeOvertimeSumByIdAsync(int id, DateTime fromDate, DateTime toDate)
        {
            var employee = await _dbContext.Employe
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
                return null;

            var overtimeRecords = await _dbContext.EmployeeOvertimes
                .Where(o => o.EmployeId == id &&
                           o.DateWorked >= fromDate &&
                           o.DateWorked <= toDate)
                .ToListAsync();

            int totalOt1Hours = overtimeRecords
                .Where(o => o.OtId == 2)
                .Sum(o => o.HoursWorked ?? 0);

            int totalOt2Hours = overtimeRecords
                .Where(o => o.OtId == 1)
                .Sum(o => o.HoursWorked ?? 0);

            return new OtSumDto
            {
                Id = employee.Id,
                Name = employee.FullName,
                FromDate = fromDate,
                ToDate = toDate,
                TotalOt1Hours = totalOt1Hours,
                TotalOt2Hours = totalOt2Hours
            };
        }

        public async Task<EmployeeOvertime> AddEmployeeOvertimeAsync(EmployeeOvertimeDto newOvertime)
        {
            

            var overtime = new EmployeeOvertime
            {
                EmployeId = newOvertime.EmployeId,
                OtId = newOvertime.OtId,
                DateWorked = newOvertime.DateWorked,
                HoursWorked = newOvertime.HoursWorked,
                Remarks = newOvertime.Remarks,
            };

            _dbContext.EmployeeOvertimes.Add(overtime);
            await _dbContext.SaveChangesAsync();
            return overtime;
        }

        public async Task<EmployeeOvertime?> UpdateEmployeeOvertimeAsync(int id, EmployeeOvertimeDto dto)
        {
            var overtime = await _dbContext.EmployeeOvertimes.FirstOrDefaultAsync(eo => eo.Id == id);
            if (overtime == null) return null;

            overtime.EmployeId = dto.EmployeId ?? overtime.EmployeId;
            overtime.OtId = dto.OtId ?? overtime.OtId;
            overtime.DateWorked = dto.DateWorked ?? overtime.DateWorked;
            overtime.HoursWorked = dto.HoursWorked ?? overtime.HoursWorked;
            overtime.Remarks = dto.Remarks ?? overtime.Remarks;

            await _dbContext.SaveChangesAsync();
            return overtime;
        }

        public async Task<EmployeeOvertime?> DeleteEmployeeOvertimeAsync(int id)
        {
            var overtime = await _dbContext.EmployeeOvertimes.FirstOrDefaultAsync(eo => eo.Id == id);
            if (overtime == null) return null;

            _dbContext.EmployeeOvertimes.Remove(overtime);
            await _dbContext.SaveChangesAsync();
            return overtime;
        }

        public async Task<List<EmployeeOvertimeDto>> GetEmployeeOvertimeByEmployeeIdAsync(int employeeId,
            string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 10)
        {
            var query = _dbContext.EmployeeOvertimes
                .Include(eo => eo.Employe)
                .Include(eo => eo.OT)
                .Where(eo => eo.EmployeId == employeeId)
                .AsQueryable();

            // Apply filters, sorting, and pagination similar to GetAll method
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                if (filterOn.Equals("OTType", StringComparison.OrdinalIgnoreCase))
                {
                    query = query.Where(eo => eo.OT != null && eo.OT.Name!.Contains(filterQuery));
                }
                else if (filterOn.Equals("HoursWorked", StringComparison.OrdinalIgnoreCase))
                {
                    if (int.TryParse(filterQuery, out int hours))
                    {
                        query = query.Where(eo => eo.HoursWorked == hours);
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                query = sortBy.ToLower() switch
                {
                    "dateworked" => isAscending ? query.OrderBy(eo => eo.DateWorked) : query.OrderByDescending(eo => eo.DateWorked),
                    "hoursworked" => isAscending ? query.OrderBy(eo => eo.HoursWorked) : query.OrderByDescending(eo => eo.HoursWorked),
                    _ => query.OrderBy(eo => eo.Id)
                };
            }

            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return await query.Select(eo => new EmployeeOvertimeDto
            {
                Id = eo.Id,
                EmployeId = eo.EmployeId,
                OtId = eo.OtId,
                DateWorked = eo.DateWorked,
                HoursWorked = eo.HoursWorked,
                Remarks = eo.Remarks,
                EmployeeName = eo.Employe != null ? eo.Employe.FullName : null,
                OTType = eo.OT != null ? eo.OT.Name : null
            }).ToListAsync();
        }

        public async Task<List<EmployeeOvertimeDto>> GetEmployeeOvertimeByDateRangeAsync(DateTime startDate, DateTime endDate,
            string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 10)
        {
            var query = _dbContext.EmployeeOvertimes
                .Include(eo => eo.Employe)
                .Include(eo => eo.OT)
                .Where(eo => eo.DateWorked >= startDate && eo.DateWorked <= endDate)
                .AsQueryable();

            // Apply filters, sorting, and pagination
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                if (filterOn.Equals("EmployeeName", StringComparison.OrdinalIgnoreCase))
                {
                    query = query.Where(eo => eo.Employe != null &&
                        (eo.Employe.FullName).Contains(filterQuery));
                }
                else if (filterOn.Equals("OTType", StringComparison.OrdinalIgnoreCase))
                {
                    query = query.Where(eo => eo.OT != null && eo.OT.Name!.Contains(filterQuery));
                }
            }

            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                query = sortBy.ToLower() switch
                {
                    "dateworked" => isAscending ? query.OrderBy(eo => eo.DateWorked) : query.OrderByDescending(eo => eo.DateWorked),
                    "hoursworked" => isAscending ? query.OrderBy(eo => eo.HoursWorked) : query.OrderByDescending(eo => eo.HoursWorked),
                    "employeename" => isAscending ? query.OrderBy(eo => eo.Employe != null ? eo.Employe.FullName : "")
                        : query.OrderByDescending(eo => eo.Employe != null ? eo.Employe.FullName : ""),
                    _ => query.OrderBy(eo => eo.Id)
                };
            }

            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return await query.Select(eo => new EmployeeOvertimeDto
            {
                Id = eo.Id,
                EmployeId = eo.EmployeId,
                OtId = eo.OtId,
                DateWorked = eo.DateWorked,
                HoursWorked = eo.HoursWorked,
                Remarks = eo.Remarks,
                EmployeeName = eo.Employe != null ? eo.Employe.FullName : null,
                OTType = eo.OT != null ? eo.OT.Name : null
            }).ToListAsync();
        }
    }
}