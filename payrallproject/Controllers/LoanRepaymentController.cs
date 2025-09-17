using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using payrallproject.Models.Domains;
using payrallproject.Models.Dtos;
using payrallproject.Services.LoanRepaymentService;
using System.ComponentModel.DataAnnotations;

namespace payrallproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanRepaymentController : ControllerBase
    {
        private readonly ILoanRepaymentService _repaymentService;

        public LoanRepaymentController(ILoanRepaymentService repaymentService)
        {
            _repaymentService = repaymentService;
        }

        [HttpPost("{loanId}/repayments")]
        public async Task<ActionResult<LoanRepayment>> AddRepayment(int loanId, [FromBody] LoanRepaymentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var repayment = await _repaymentService.AddRepaymentAsync(loanId, dto.InstallmentAmount, dto.PaymentDate, dto.Description);
                return Ok(repayment);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{loanId}/repayments")]
        public async Task<ActionResult<List<LoanRepayment>>> GetRepayments(int loanId)
        {
            var repayments = await _repaymentService.GetRepaymentsByLoanIdAsync(loanId);
            return Ok(repayments);
        }

        [HttpGet("{loanId}/next-payment")]
        public async Task<ActionResult<decimal>> GetNextPaymentAmount(int loanId)
        {
            var amount = await _repaymentService.CalculateNextPaymentAmountAsync(loanId);
            return Ok(amount);
        }
    }
}