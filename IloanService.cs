namespace Beadandó
{
    public interface ILoanService
    {
        void Add(Loan loan);

        void Delete(Guid BookId);

        Loan Get(Guid BookID);

        List<Loan> GetAll();

        void Update(Loan newLoan);
    }
}
