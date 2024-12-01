using BeadandóShared;
using System;
using Beadandó.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Beadandó
{
    public class BookService : IBookService
    {
        private BookContext _context;
        private ILogger<BookService> _logger;

        public BookService(ILogger<BookService> logger, BookContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task AddAsync(Book book)
        {
            _logger.LogInformation("Book to add: {@Book}", book);

            await _context.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var book = await GetAsync(id);

            if (book is null)
            {
                throw new KeyNotFoundException("Book not found");
            }

            _context.RemoveRange(book);
            _context.Remove(book);
            await _context.SaveChangesAsync();
        }

        public async Task<Book> GetAsync(Guid id)
        {
            return await _context.FindAsync<Book>(id);
        }

        public async Task<List<Book>> GetAllAsync()
        {
            _logger.LogInformation("All books retrieved");
            return await _context.Books.ToListAsync();
        }

        public async Task UpdateAsync(Book newBook)
        {
            var existingBook = await GetAsync(newBook.Id);

            existingBook.Title = newBook.Title;
            existingBook.Author = newBook.Author;
            existingBook.Publisher = newBook.Publisher;
            existingBook.PublishDate = newBook.PublishDate;



            await _context.SaveChangesAsync();
        }
    }
}
