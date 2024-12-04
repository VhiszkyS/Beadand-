using BeadandóShared;
using Beadandó.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Beadandó
{
    public class LoanService : ILoanService
    {
        private LoanContext _context;
        private ILogger<LoanService> _logger;

        public LoanService(ILogger<LoanService> logger, LoanContext context)
        {
            _logger = logger;
            _context = context;
        }



        public async Task AddAsync(Loan loan)
        {
            _logger.LogInformation("Loan to add: {@Loan}", loan);

            await _context.AddAsync(loan);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var loan = await GetAsync(id);

            if (loan is null)
            {
                throw new KeyNotFoundException("Loan not found");
            }

            _context.RemoveRange(loan);
            _context.Remove(loan);
            await _context.SaveChangesAsync();
        }

        public async Task<Loan> GetAsync(Guid id)
        {
            return await _context.FindAsync<Loan>(id);
        }

        public async Task<List<Loan>> GetAllAsync()
        {
            _logger.LogInformation("All loans retrieved");
            return await _context.Loans.ToListAsync();
        }

        public async Task UpdateAsync(Loan newLoan)
        {
            var existingLoan = await GetAsync(newLoan.Id);

            existingLoan.LoanDate = newLoan.LoanDate;
            existingLoan.Deadline = newLoan.Deadline;
            
            await _context.SaveChangesAsync();
        }
    }
}
