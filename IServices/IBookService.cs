using Beadandó.Shared;
using System;

namespace Beadandó
{
    public interface IBookService
    {
        Task AddAsync (Book book);

        Task DeleteAsync (Guid Id);

        Task <Book> GetAsync (Guid Id);

        Task <List<Book>> GetAllAsync ();

        Task UpdateAsync (Book newBook);
    }
}
