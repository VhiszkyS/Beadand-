using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Beadandó;
using Beadandó.Contexts;
using Beadandó.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

public class BookServiceTests
{
    private readonly Mock<ILogger<BookService>> _loggerMock;
    private readonly DbContextOptions<BookContext> _dbContextOptions;
    private readonly BookContext _context;
    private readonly BookService _service;

    public BookServiceTests()
    {
        // Mock logger
        _loggerMock = new Mock<ILogger<BookService>>();

        // InMemory database setup
        _dbContextOptions = new DbContextOptionsBuilder<BookContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new BookContext(_dbContextOptions);

        // BookService instance
        _service = new BookService(_loggerMock.Object, _context);
    }

    [Fact]
    public async Task AddAsync_Should_Add_Book_To_Database()
    {
        // Arrange
        var book = new Book
        {
            Id = Guid.NewGuid(),
            Title = "Test Book",
            Author = "Test Author",
            Publisher = "Test Publisher",
            PublishDate = 2022
        };

        // Act
        await _service.AddAsync(book);

        // Assert
        var result = await _context.Books.FindAsync(book.Id);
        Assert.NotNull(result);
        Assert.Equal(book.Title, result.Title);
        Assert.Equal(book.Author, result.Author);
    }

    [Fact]
    public async Task DeleteAsync_Should_Remove_Book_From_Database()
    {
        // Arrange
        var book = new Book
        {
            Id = Guid.NewGuid(),
            Title = "Delete Test Book",
            Author = "Delete Test Author",
            Publisher = "Delete Test Publisher",
            PublishDate = 2020
        };

        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        // Act
        await _service.DeleteAsync(book.Id);

        // Assert
        var result = await _context.Books.FindAsync(book.Id);
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAsync_Should_Return_Book_If_Exists()
    {
        // Arrange
        var book = new Book
        {
            Id = Guid.NewGuid(),
            Title = "Existing Book",
            Author = "Existing Author",
            Publisher = "Existing Publisher",
            PublishDate = 2018
        };

        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        // Act
        var result = await _service.GetAsync(book.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(book.Id, result.Id);
        Assert.Equal(book.Title, result.Title);
    }

    [Fact]
    public async Task GetAllAsync_Should_Return_All_Books()
    {
        // Arrange
        var book1 = new Book { Id = Guid.NewGuid(), Title = "Book 1", Author = "Author 1", Publisher = "Publisher 1", PublishDate =  2010 };
        var book2 = new Book { Id = Guid.NewGuid(), Title = "Book 2", Author = "Author 2", Publisher = "Publisher 2", PublishDate = 2015 };

        _context.Books.AddRange(book1, book2);
        await _context.SaveChangesAsync();

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task UpdateAsync_Should_Update_Book_Details()
    {
        // Arrange
        var book = new Book
        {
            Id = Guid.NewGuid(),
            Title = "Original Title",
            Author = "Original Author",
            Publisher = "Original Publisher",
            PublishDate = 2000
        };

        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        var updatedBook = new Book
        {
            Id = book.Id,
            Title = "Updated Title",
            Author = "Updated Author",
            Publisher = "Updated Publisher",
            PublishDate = 2022
        };

        // Act
        await _service.UpdateAsync(updatedBook);

        // Assert
        var result = await _context.Books.FindAsync(book.Id);
        Assert.NotNull(result);
        Assert.Equal(updatedBook.Title, result.Title);
        Assert.Equal(updatedBook.Author, result.Author);
        Assert.Equal(updatedBook.PublishDate, result.PublishDate);
    }
}

