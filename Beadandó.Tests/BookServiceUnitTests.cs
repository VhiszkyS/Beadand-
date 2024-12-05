using FluentAssertions.Execution;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beadandó.Contexts;
using Beadandó.Shared;

namespace Beadandó.Tests
{
    public sealed class BookServiceUnitTests : IAsyncDisposable
    {
        private readonly BookContext _inMemoryDbContext;

        public BookServiceUnitTests()
        {
            var contextOptions = new DbContextOptionsBuilder<BookContext>()
                .UseInMemoryDatabase("BeadandóTests")
                .Options;

            _inMemoryDbContext = new BookContext(contextOptions);

            _inMemoryDbContext.Database.EnsureDeleted();
            _inMemoryDbContext.Database.EnsureCreated();

            _inMemoryDbContext.SaveChanges();
        }

        [Fact]
        public async Task IT_AddAsync_ContainsOnePerson()
        {
            // Arrange
            var bookService = new BookService(NullLogger<BookService>.Instance, _inMemoryDbContext);

            await bookService.AddAsync(new Book
            {
                Title = "IT",
                Author = "Stephen King",
                Publisher = "Líra",
                PublishDate = 1958,
                Items = [],
            });

            // Act
            var books = await bookService.GetAllAsync();

            // Assert
            Assert.Single(books);
        }

        [Fact]
        public async Task LordOfTheRings_AddAsync_ContainsOneBook()
        {
            // Arrange
            var bookService = new BookService(NullLogger<BookService>.Instance, _inMemoryDbContext);

            await bookService.AddAsync(new Book
            {
                Title = "Lord of the Rings",
                Author = "Tolkien",
                Publisher = "Könyvmoly",
                PublishDate = 1940,
                Items = [],
            });

            // Act
            var books = await bookService.GetAllAsync();

            // Assert
            Assert.Single(books);
        }

        public async ValueTask DisposeAsync()
        {
            await _inMemoryDbContext.DisposeAsync();
        }
    }
}
