using Microsoft.AspNetCore.Mvc;
using payrallproject.Models.Domains;
using payrallproject.Models.Dtos;
using payrallproject.Services.JobRoleService;

namespace payrallproject.Controllers
{
    [Route("api/jobroles")]
    [ApiController]
    public class JobRolesController : ControllerBase
    {
        private readonly IJobRoleService _jobRoleService;

        public JobRolesController(IJobRoleService jobRoleService)
        {
            _jobRoleService = jobRoleService;
        }

        [HttpGet]
        public async Task<ActionResult<List<JobRoleDto>>> GetAll(
            string? filterOn, string? filterQuery,
            string? sortBy, bool isAscending = true,
            int pageNumber = 1, int pageSize = 10)
        {
            var jobRoles = await _jobRoleService.GetAllJobRolesAsync(filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);
            return Ok(jobRoles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<JobRoleDto>> GetById(int id)
        {
            var jobRole = await _jobRoleService.GetJobRoleByIdAsync(id);
            if (jobRole == null) return NotFound();
            return Ok(jobRole);
        }

        [HttpGet("department/{departmentId}")]
        public async Task<ActionResult<List<JobRoleDto>>> GetByDepartmentId(int departmentId)
        {
            var jobRoles = await _jobRoleService.GetJobRolesByDepartmentIdAsync(departmentId);
            return Ok(jobRoles);
        }

        [HttpPost]
        public async Task<ActionResult<JobRole>> Create(JobRoleDto dto)
        {
            var jobRole = await _jobRoleService.AddJobRoleAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = jobRole.Id }, jobRole);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<JobRole>> Update(int id, JobRoleDto dto)
        {
            var updated = await _jobRoleService.UpdateJobRoleAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<JobRole>> Delete(int id)
        {
            var deleted = await _jobRoleService.DeleteJobRoleAsync(id);
            if (deleted == null) return NotFound();
            return Ok(deleted);
        }
    }
}
