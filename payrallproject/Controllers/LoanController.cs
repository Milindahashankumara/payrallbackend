using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using payrallproject.Models.Domains;
using payrallproject.Models.Dtos;
using payrallproject.Services.LoanService;

namespace payrallproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly ILoanService _loanService;

        public LoanController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Loans>>> GetAll(
            string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 10)
        {
            var loans = await _loanService.GetAllLoansAsync(filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);
            return Ok(loans);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Loans>> GetById(int id)
        {
            var loan = await _loanService.GetLoansByIdAsync(id);
            if (loan == null) return NotFound();
            return Ok(loan);
        }

        [HttpPost]
        public async Task<ActionResult<Loans>> Create(LoansDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var loan = await _loanService.AddLoansAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = loan.Id }, loan);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Loans>> Update(int id, LoansDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _loanService.UpdateLoansAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _loanService.DeleteLoansAsync(id);
            if (deleted == null) return NotFound();
            return NoContent();
        }

        [HttpGet("deleted")]
        public async Task<ActionResult<List<Loans>>> GetAllDeleted(
            string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 10)
        {
            var loans = await _loanService.GetAllDeletedLoansAsync(filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);
            return Ok(loans);
        }

        [HttpGet("deleted/{id}")]
        public async Task<ActionResult<Loans>> GetDeletedById(int id)
        {
            var loan = await _loanService.GetDeletedLoansByIdAsync(id);
            if (loan == null) return NotFound();
            return Ok(loan);
        }

        [HttpPost("recover/{id}")]
        public async Task<ActionResult<Loans>> Recover(int id)
        {
            var recovered = await _loanService.RecoverDeletedLoansAsync(id);
            if (recovered == null) return NotFound();
            return Ok(recovered);
        }
    }
}