using Microsoft.AspNetCore.Mvc;

namespace Beadandó.Controllers
{
    [ApiController]
    [Route("loans")]
    public class LoanController : ControllerBase 
    {
        private ILoanService _loanService;

        public LoanController (ILoanService loanService) 
        {
            _loanService = loanService;
        }

        [HttpPost]
        public async Task <IActionResult> Add([FromBody] Loan loan) 
        {
            var existingLoan = await _loanService.GetAsync(loan.BookId);

            if (existingLoan != null) 
            {
                return Conflict();
            }

            await _loanService.AddAsync(loan);

            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public async Task <IActionResult> Delete(Guid id) 
        {
            var loan = await _loanService.GetAsync(id);

            if (loan == null) 
            {
                return NotFound();
            }

            await _loanService.DeleteAsync(id);

            return Ok();
        }

        [HttpGet("{id:guid}")]
        public async Task <IActionResult> Get(Guid id) 
        {
            var loan = await _loanService.GetAsync(id);

            if (loan == null) 
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpGet]
        public async Task <ActionResult<List<Loan>>> Get()
        {
            return Ok(await _loanService.GetAllAsync());
        }

        [HttpPut("{id:guid}")]
        public async Task <IActionResult> Update(Guid id, [FromBody] Loan newLoan)
        {
            if (id != newLoan.BookId)
            {
                return BadRequest();
            }

            var existingLoan = await _loanService.GetAsync(id);

            if (existingLoan is null)
            {
                return NotFound();
            }

           await _loanService.UpdateAsync(newLoan);

            return Ok();
        }
    }
}
