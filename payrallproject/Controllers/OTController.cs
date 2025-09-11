using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using payrallproject.Models.Dtos;
using payrallproject.Services.EmployeeService;
using payrallproject.Services.OTService;

namespace payrallproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OTController : ControllerBase
    {
        private readonly IOTService _otService;
        public OTController(IOTService otService)
        {
            _otService = otService;
        }

        [HttpPost]
        public async Task<IActionResult> AddOT([FromBody] OTDto otDto)
        {
            if (otDto == null) return BadRequest();

            var added = await _otService.AddOTAsync(otDto);
            return Ok(added);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOT(
            [FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var ots = await _otService.GetAllOTAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);
            return Ok(ots);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOTById([FromRoute] int id)
        {
            var ot = await _otService.GetOTByIdAsync(id);
            if (ot == null) return NotFound();

            return Ok(ot);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOT([FromRoute] int id, [FromBody] OTDto otDto)
        {
            var updated = await _otService.UpdateOTAsync(id, otDto);
            if (updated == null) return NotFound();

            return Ok(updated);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOT([FromRoute] int id)
        {
            var deleted = await _otService.DeleteOTAsync(id);
            if (deleted == null) return NotFound();

            return Ok(deleted);
        }

        [HttpGet("deleted")]
        public async Task<IActionResult> GetDeletedOT(
            [FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var ots = await _otService.GetAllDeletedOTAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);
            return Ok(ots);
        }

        [HttpGet("deleted/{id:int}")]
        public async Task<IActionResult> GetDeletedOTById([FromRoute] int id)
        {
            var ot = await _otService.GetDeletedOTByIdAsync(id);
            if (ot == null) return NotFound();

            return Ok(ot);
        }

        [HttpPut("recover/{id:int}")]
        public async Task<IActionResult> RecoverDeletedOT([FromRoute] int id)
        {
            var ot = await _otService.RecoverDeletedOTAsync(id);
            if (ot == null) return NotFound();

            return Ok(ot);
        }
    }
}
