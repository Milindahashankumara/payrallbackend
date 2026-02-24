using Microsoft.EntityFrameworkCore;
using payrallproject.Data;
using payrallproject.Models.Domains;
using payrallproject.Models.Dtos;

namespace payrallproject.Services.JobRoleService
{
    public class JobRoleService : IJobRoleService
    {
        private readonly AuthDbContext _dbContext;

        public JobRoleService(AuthDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<JobRoleDto>> GetAllJobRolesAsync(
            string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 10)
        {
            var query = _dbContext.JobRoles
                .Include(j => j.Department)
                .Include(j => j.EmployeeCategories)
                .Where(j => j.IsActive == true)
                .AsQueryable();

            // Filtering
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                if (filterOn.Equals("RoleName", StringComparison.OrdinalIgnoreCase))
                {
                    query = query.Where(j => j.RoleName!.Contains(filterQuery));
                }
                else if (filterOn.Equals("DepartmentName", StringComparison.OrdinalIgnoreCase))
                {
                    query = query.Where(j => j.Department != null && j.Department.DepartmentName!.Contains(filterQuery));
                }
            }

            // Sorting
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                query = sortBy.ToLower() switch
                {
                    "rolename" => isAscending ? query.OrderBy(j => j.RoleName) : query.OrderByDescending(j => j.RoleName),
                    "departmentname" => isAscending ? query.OrderBy(j => j.Department!.DepartmentName) : query.OrderByDescending(j => j.Department!.DepartmentName),
                    _ => query.OrderBy(j => j.Id)
                };
            }

            // Pagination
            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return await query.Select(j => new JobRoleDto
            {
                Id = j.Id,
                RoleName = j.RoleName,
                DepartmentId = j.DepartmentId,
                DepartmentName = j.Department != null ? j.Department.DepartmentName : null,
                EmployeeCategoriesId = j.EmployeeCategoriesId,
                EmployeeCategoriesName = j.EmployeeCategories != null ? j.EmployeeCategories.CategoryName : null,
                IsActive = j.IsActive
            }).ToListAsync();
        }

        public async Task<JobRoleDto?> GetJobRoleByIdAsync(int id)
        {
            return await _dbContext.JobRoles
                .Include(j => j.Department)
                .Include(j => j.EmployeeCategories)
                .Where(j => j.Id == id && j.IsActive == true)
                .Select(j => new JobRoleDto
                {
                    Id = j.Id,
                    RoleName = j.RoleName,
                    DepartmentId = j.DepartmentId,
                    DepartmentName = j.Department.DepartmentName,
                    EmployeeCategoriesId = j.EmployeeCategoriesId,
                    EmployeeCategoriesName = j.EmployeeCategories.CategoryName,
                    IsActive = j.IsActive
                })
                .FirstOrDefaultAsync();
        }

        public async Task<List<JobRoleDto>> GetJobRolesByDepartmentIdAsync(int departmentId)
        {
            return await _dbContext.JobRoles
                .Include(j => j.Department)
                .Include(j => j.EmployeeCategories)
                .Where(j => j.DepartmentId == departmentId && j.IsActive == true)
                .Select(j => new JobRoleDto
                {
                    Id = j.Id,
                    RoleName = j.RoleName,
                    DepartmentId = j.DepartmentId,
                    DepartmentName = j.Department.DepartmentName,
                    EmployeeCategoriesId = j.EmployeeCategoriesId,
                    EmployeeCategoriesName = j.EmployeeCategories.CategoryName,
                    IsActive = j.IsActive
                })
                .ToListAsync();
        }

        public async Task<JobRole> AddJobRoleAsync(JobRoleDto newJobRole)
        {
            var jobRole = new JobRole
            {
                RoleName = newJobRole.RoleName!,
                DepartmentId = newJobRole.DepartmentId,
                EmployeeCategoriesId = newJobRole.EmployeeCategoriesId,
                IsActive = newJobRole.IsActive ?? true
            };

            _dbContext.JobRoles.Add(jobRole);
            await _dbContext.SaveChangesAsync();
            return jobRole;
        }

        public async Task<JobRole?> UpdateJobRoleAsync(int id, JobRoleDto dto)
        {
            var jobRole = await _dbContext.JobRoles.FirstOrDefaultAsync(j => j.Id == id && j.IsActive == true);
            if (jobRole == null) return null;

            jobRole.RoleName = dto.RoleName ?? jobRole.RoleName;
            jobRole.DepartmentId = dto.DepartmentId ?? jobRole.DepartmentId;
            jobRole.EmployeeCategoriesId = dto.EmployeeCategoriesId ?? jobRole.EmployeeCategoriesId;
            jobRole.IsActive = dto.IsActive ?? jobRole.IsActive;

            await _dbContext.SaveChangesAsync();
            return jobRole;
        }

        public async Task<JobRole?> DeleteJobRoleAsync(int id)
        {
            var jobRole = await _dbContext.JobRoles.FirstOrDefaultAsync(j => j.Id == id && j.IsActive == true);
            if (jobRole == null) return null;

            jobRole.IsActive = false;
            await _dbContext.SaveChangesAsync();
            return jobRole;
        }
    }
}
