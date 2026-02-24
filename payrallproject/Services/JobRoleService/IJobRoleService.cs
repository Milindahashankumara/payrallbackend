using payrallproject.Models.Domains;
using payrallproject.Models.Dtos;

namespace payrallproject.Services.JobRoleService
{
    public interface IJobRoleService
    {
        Task<List<JobRoleDto>> GetAllJobRolesAsync(
            string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 10);
        Task<JobRoleDto?> GetJobRoleByIdAsync(int id);
        Task<List<JobRoleDto>> GetJobRolesByDepartmentIdAsync(int departmentId);
        Task<JobRole> AddJobRoleAsync(JobRoleDto newJobRole);
        Task<JobRole?> UpdateJobRoleAsync(int id, JobRoleDto jobRoleDto);
        Task<JobRole?> DeleteJobRoleAsync(int id);
    }
}
