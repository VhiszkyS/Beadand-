using BeadandóShared;
using System;

namespace Beadandó.UI.Services;

public interface ILoanService
{
    public Task<List<Loan>> GetAllAsync();

    public Task AddAsync(Loan loan);

    public Task<Loan> GetAsync(Guid BookId);

    public Task DeleteAsync(Guid BookId);

    public Task UpdateAsync(Loan loan);
}
