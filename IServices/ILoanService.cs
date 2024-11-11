namespace Beadandó
{
    public interface ILoanService
    {
        Task AddAsync (Loan loan);

        Task DeleteAsync (Guid BookId);

        Task <Loan> GetAsync(Guid BookID);

        Task <List<Loan>> GetAllAsync();

        Task UpdateAsync(Loan newLoan);
    }
}
