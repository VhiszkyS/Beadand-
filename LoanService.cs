namespace Beadandó
{
    public class LoanService : ILoanService
    {
        private List<Loan> _loanList = new List<Loan>();

        public void Add(Loan loan) 
        { 
            _loanList.Add(loan);
        }

        public void Delete(Guid BookId) 
        {
            _loanList.RemoveAll(p=>p.BookId == BookId);
        }

        public Loan Get(Guid BookId) 
        {
            return _loanList.Find(p=>p.BookId == BookId);
        }

        public List<Loan> GetAll() 
        {
            return _loanList;
        }

        public void Update(Loan newLoan)
        {
            var existingLoan = Get(newLoan.ReaderId);
            existingLoan.BookId = newLoan.BookId;
            existingLoan.LoanDate = newLoan.LoanDate;
            existingLoan.Deadline = newLoan.Deadline;
        }
    }
}
