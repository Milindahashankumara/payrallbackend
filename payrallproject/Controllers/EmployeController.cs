using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using payrallproject.Models.Dtos;
using payrallproject.Services.EmployeeService;

namespace payrallproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] EmployeDto employeDto)
        {
            if (employeDto == null)
                return BadRequest();

            var addedEmployee = await _employeeService.AddEmployeAsync(employeDto);
            return Ok(addedEmployee);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees(
            [FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var AllEmployees = await _employeeService.GetAllEmployeesAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);
            return Ok(AllEmployees);
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetAllEmployeesById([FromRoute] int id)
        {
            var SelectedEmployee = await _employeeService.GetEmployeByIdAsync(id);
            if (SelectedEmployee == null)
            {
                return NotFound();
            }
            return Ok(SelectedEmployee);
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] int id, [FromBody] EmployeDto employeeDto)
        {
            var SelectedEmployee = await _employeeService.UpdateEmployeAsync(id, employeeDto);
            if (SelectedEmployee == null)
            {
                return NotFound();
            }
            return Ok(SelectedEmployee);
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] int id)
        {
            var SelectedEmployee = await _employeeService.DeleteEmployeAsync(id);
            if (SelectedEmployee == null)
            {
                return NotFound();
            }
            return Ok(SelectedEmployee);
        }
        [HttpGet("getAllDeletedEmployees")]
        public async Task<IActionResult> GetDeletedEmployees(
            [FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var AllEmployees = await _employeeService.GetAllDeletedEmployesAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);
            return Ok(AllEmployees);
        }
        [HttpGet("getDeletedEmployeeById/{id:int}")]
        public async Task<IActionResult> GetDeletedAssetById([FromRoute] int id)
        {
            var SelectedEmployee = await _employeeService.GetDeletedEmployeByIdAsync(id);
            if (SelectedEmployee == null)
            {
                return NotFound();
            }
            return Ok(SelectedEmployee);
        }
        [HttpPut("recoverDeletedEmployee/{id:int}")]
        public async Task<IActionResult> RecoverDeletedEmployee([FromRoute] int id)
        {
            var SelectedEmployee = await _employeeService.RecoverDeletedEmployeAsync(id);
            if (SelectedEmployee == null)
            {
                return NotFound();
            }
            return Ok(SelectedEmployee);
        }
        [HttpGet]
        [Route("employeeshaveleaves")]
        public async Task<IActionResult> GetAllEmployeesHaveLeaves(
            [FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var AllEmployees = await _employeeService.GetAllEmployeesAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);
            return Ok(AllEmployees);
        }
        [HttpGet]
        [Route("employeeshaveleaves{id:int}")]
        public async Task<IActionResult> GetAllEmployeesHaveLeavesById([FromRoute] int id)
        {
            var SelectedEmployee = await _employeeService.GetEmployeByIdAsync(id);
            if (SelectedEmployee == null)
            {
                return NotFound();
            }
            return Ok(SelectedEmployee);
        }
    }
}
