using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using payrallproject.Models.Domains;
using payrallproject.Models.Dtos;
using payrallproject.Services.DepartmentService;

namespace payrallproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Department>>> GetAll(
            string? filterOn, string? filterQuery,
            string? sortBy, bool isAscending = true,
            int pageNumber = 1, int pageSize = 10)
        {
            var departments = await _departmentService.GetAllDepartmentAsync(filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);
            return Ok(departments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetById(int id)
        {
            var dept = await _departmentService.GetDepartmentByIdAsync(id);
            if (dept == null) return NotFound();
            return Ok(dept);
        }

        [HttpPost]
        public async Task<ActionResult<Department>> Create(DepartmentDto dto)
        {
            var dept = await _departmentService.AddDepartmentAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = dept.Id }, dept);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Department>> Update(int id, DepartmentDto dto)
        {
            var updated = await _departmentService.UpdateDepartmentAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Department>> Delete(int id)
        {
            var deleted = await _departmentService.DeleteDepartmentAsync(id);
            if (deleted == null) return NotFound();
            return Ok(deleted);
        }

        [HttpGet("deleted")]
        public async Task<ActionResult<List<Department>>> GetDeleted(
            string? filterOn, string? filterQuery,
            string? sortBy, bool isAscending = true,
            int pageNumber = 1, int pageSize = 10)
        {
            var departments = await _departmentService.GetAllDeletedDepartmentAsync(filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);
            return Ok(departments);
        }

        [HttpGet("deleted/{id}")]
        public async Task<ActionResult<Department>> GetDeletedById(int id)
        {
            var dept = await _departmentService.GetDeletedDepartmentByIdAsync(id);
            if (dept == null) return NotFound();
            return Ok(dept);
        }

        [HttpPost("recover/{id}")]
        public async Task<ActionResult<Department>> Recover(int id)
        {
            var dept = await _departmentService.RecoverDeletedDepartmentAsync(id);
            if (dept == null) return NotFound();
            return Ok(dept);
        }
    }
}
