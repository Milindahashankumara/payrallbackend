using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using payrallproject.Models.Domains;
using payrallproject.Models.Dtos;
using payrallproject.Services.NoPayDayService;

namespace payrallproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoPayDayController : ControllerBase
    {
        private readonly INoPayDayService _noPayDayService;

        public NoPayDayController(INoPayDayService noPayDayService)
        {
            _noPayDayService = noPayDayService;
        }

        [HttpPost]
        public async Task<ActionResult<NoPayDay>> CreateNoPayDay([FromBody] NoPayDayDto noPayDayDto)
        {
            try
            {
                var noPayDay = await _noPayDayService.CreateNoPayDayAsync(noPayDayDto);
                return Ok(noPayDay);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("employee/{employeID}")]
        public async Task<ActionResult<List<NoPayDay>>> GetNoPayDaysByEmployee(int employeID)
        {
            try
            {
                var noPayDays = await _noPayDayService.GetNoPayDaysByEmployeeAsync(employeID);
                return Ok(noPayDays);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("employee/{employeID}/{year}/{month}")]
        public async Task<ActionResult<List<NoPayDay>>> GetNoPayDaysByEmployeeAndMonth(int employeID, int year, int month)
        {
            try
            {
                var noPayDays = await _noPayDayService.GetNoPayDaysByEmployeeAndMonthAsync(employeID, year, month);
                return Ok(noPayDays);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNoPayDay(int id)
        {
            try
            {
                var result = await _noPayDayService.DeleteNoPayDayAsync(id);
                if (!result) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
