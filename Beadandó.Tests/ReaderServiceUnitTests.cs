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

public class ReaderServiceTests
{
    private readonly Mock<ILogger<ReaderService>> _loggerMock;
    private readonly DbContextOptions<ReaderContext> _dbContextOptions;
    private readonly ReaderContext _context;
    private readonly ReaderService _service;

    public ReaderServiceTests()
    {
        // Mock logger
        _loggerMock = new Mock<ILogger<ReaderService>>();

        // InMemory database setup
        _dbContextOptions = new DbContextOptionsBuilder<ReaderContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new ReaderContext(_dbContextOptions);

        // ReaderService instance
        _service = new ReaderService(_loggerMock.Object, _context);
    }

    [Fact]
    public async Task AddAsync_Should_Add_Reader_To_Database()
    {
        // Arrange
        var reader = new Reader
        {
            Id = Guid.NewGuid(),
            Name = "John Doe",
            Address = "123 Main St",
            BirthDate = new DateOnly(1985, 5, 15)
        };

        // Act
        await _service.AddAsync(reader);

        // Assert
        var result = await _context.Readers.FindAsync(reader.Id);
        Assert.NotNull(result);
        Assert.Equal(reader.Name, result.Name);
        Assert.Equal(reader.Address, result.Address);
    }

    [Fact]
    public async Task DeleteAsync_Should_Remove_Reader_From_Database()
    {
        // Arrange
        var reader = new Reader
        {
            Id = Guid.NewGuid(),
            Name = "Jane Smith",
            Address = "456 Elm St",
            BirthDate = new DateOnly(1990, 8, 25)
        };

        _context.Readers.Add(reader);
        await _context.SaveChangesAsync();

        // Act
        await _service.DeleteAsync(reader.Id);

        // Assert
        var result = await _context.Readers.FindAsync(reader.Id);
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAsync_Should_Return_Reader_If_Exists()
    {
        // Arrange
        var reader = new Reader
        {
            Id = Guid.NewGuid(),
            Name = "Alice Johnson",
            Address = "789 Oak St",
            BirthDate = new DateOnly(1978, 3, 10)
        };

        _context.Readers.Add(reader);
        await _context.SaveChangesAsync();

        // Act
        var result = await _service.GetAsync(reader.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(reader.Id, result.Id);
        Assert.Equal(reader.Name, result.Name);
    }

    [Fact]
    public async Task GetAllAsync_Should_Return_All_Readers()
    {
        // Arrange
        var reader1 = new Reader { Id = Guid.NewGuid(), Name = "Tom", Address = "Street 1", BirthDate = new DateOnly(1980, 1, 1) };
        var reader2 = new Reader { Id = Guid.NewGuid(), Name = "Jerry", Address = "Street 2", BirthDate = new DateOnly(1990, 2, 2) };

        _context.Readers.AddRange(reader1, reader2);
        await _context.SaveChangesAsync();

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task UpdateAsync_Should_Update_Reader_Details()
    {
        // Arrange
        var reader = new Reader
        {
            Id = Guid.NewGuid(),
            Name = "Initial Name",
            Address = "Initial Address",
            BirthDate = new DateOnly(2000, 1, 1)
        };

        _context.Readers.Add(reader);
        await _context.SaveChangesAsync();

        var updatedReader = new Reader
        {
            Id = reader.Id,
            Name = "Updated Name",
            Address = "Updated Address",
            BirthDate = new DateOnly(1999, 12, 31)
        };

        // Act
        await _service.UpdateAsync(updatedReader);

        // Assert
        var result = await _context.Readers.FindAsync(reader.Id);
        Assert.NotNull(result);
        Assert.Equal(updatedReader.Name, result.Name);
        Assert.Equal(updatedReader.Address, result.Address);
        Assert.Equal(updatedReader.BirthDate, result.BirthDate);
    }
}
