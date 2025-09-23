using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using payrallproject.Models.Dtos;
using payrallproject.Services.HolidayService;

namespace payrallproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HolidayController : ControllerBase
    {
        private readonly IHolidayService _holidayService;

        public HolidayController(IHolidayService holidayService)
        {
            _holidayService = holidayService;
        }

        [HttpGet]
        public async Task<ActionResult<List<HolidayDto>>> GetAllHolidays()
        {
            try
            {
                var holidays = await _holidayService.GetAllHolidaysAsync();
                return Ok(holidays);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving holidays.", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HolidayDto>> GetHoliday(int id)
        {
            try
            {
                var holiday = await _holidayService.GetHolidayByIdAsync(id);
                if (holiday == null)
                    return NotFound(new { message = "Holiday not found." });

                return Ok(holiday);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the holiday.", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<HolidayDto>> CreateHoliday([FromBody] CreateHolidayDto createHolidayDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var holiday = await _holidayService.CreateHolidayAsync(createHolidayDto);
                return CreatedAtAction(nameof(GetHoliday), new { id = holiday.Id }, holiday);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the holiday.", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<HolidayDto>> UpdateHoliday(int id, [FromBody] UpdateHolidayDto updateHolidayDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var holiday = await _holidayService.UpdateHolidayAsync(id, updateHolidayDto);
                if (holiday == null)
                    return NotFound(new { message = "Holiday not found." });

                return Ok(holiday);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the holiday.", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHoliday(int id)
        {
            try
            {
                var result = await _holidayService.DeleteHolidayAsync(id);
                if (!result)
                    return NotFound(new { message = "Holiday not found." });

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the holiday.", error = ex.Message });
            }
        }

        [HttpGet("range")]
        public async Task<ActionResult<List<HolidayDto>>> GetHolidaysByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            try
            {
                if (startDate > endDate)
                    return BadRequest(new { message = "Start date cannot be after end date." });

                var holidays = await _holidayService.GetHolidaysByDateRangeAsync(startDate, endDate);
                return Ok(holidays);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving holidays.", error = ex.Message });
            }
        }

        [HttpGet("count")]
        public async Task<ActionResult<int>> GetHolidaysCount([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            try
            {
                if (startDate > endDate)
                    return BadRequest(new { message = "Start date cannot be after end date." });

                var count = await _holidayService.GetHolidaysCountAsync(startDate, endDate);
                return Ok(count);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while counting holidays.", error = ex.Message });
            }
        }

        [HttpGet("countnonweekend")]
        public async Task<ActionResult<int>> GetNonWeekendHolidayCount([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            try
            {
                if (startDate > endDate)
                    return BadRequest(new { message = "Start date cannot be after end date." });

                var count = await _holidayService.GetNonWeekendHolidayCountAsync(startDate, endDate);
                return Ok(count);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while counting holidays.", error = ex.Message });
            }
        }

        [HttpGet("year/{year}")]
        public async Task<ActionResult<List<HolidayDto>>> GetHolidaysByYear(int year)
        {
            try
            {
                var holidays = await _holidayService.GetHolidaysByYearAsync(year);
                return Ok(holidays);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving holidays.", error = ex.Message });
            }
        }
    }
}