using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using payrallproject.Models.Domains;
using payrallproject.Models.Dtos;
using payrallproject.Services.EmployeeCategoriesService;

namespace payrallproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeCategoriesController : ControllerBase
    {
        private readonly IEmployeeCategoriesService _service;

        public EmployeeCategoriesController(IEmployeeCategoriesService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<EmployeeCategories>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeCategories>> GetById(int id)
        {
            var category = await _service.GetByIdAsync(id);
            if (category == null) return NotFound();
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeCategories>> Create(EmployeeCategoriesDto dto)
        {
            var category = await _service.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EmployeeCategories>> Update(int id, EmployeeCategoriesDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (deleted == null) return NotFound();
            return NoContent();
        }
    }
}
