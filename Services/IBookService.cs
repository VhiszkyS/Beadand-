using BeadandóShared;
using System;

namespace BeadandóUI.Services;

public interface IBookService
{
    public Task<List<Book>> GetAllAsync();

    public Task AddAsync(Book book);

    public Task<Book> GetAsync(Guid id);

    public Task DeleteAsync(Guid id);

    public Task UpdateAsync(Book book);
}
