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
        public IActionResult Add([FromBody] Loan loan) 
        {
            var existingLoan = _loanService.Get(loan.BookId);

            if (existingLoan != null) 
            {
                return Conflict();
            }

            _loanService.Add(loan);

            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id) 
        {
            var loan = _loanService.Get(id);

            if (loan == null) 
            {
                return NotFound();
            }

            _loanService.Delete(id);

            return Ok();
        }

        [HttpGet("{id:guid}")]
        public IActionResult Get(Guid id) 
        {
            var loan = _loanService.Get(id);

            if (loan == null) 
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpGet]
        public ActionResult<List<Loan>> Get()
        {
            return Ok(_loanService.GetAll());
        }

        [HttpPut("{id:guid}")]
        public IActionResult Update(Guid id, [FromBody] Loan newLoan)
        {
            if (id != newLoan.BookId)
            {
                return BadRequest();
            }

            var existingLoan = _loanService.Get(id);

            if (existingLoan is null)
            {
                return NotFound();
            }

            _loanService.Update(newLoan);

            return Ok();
        }
    }
}
