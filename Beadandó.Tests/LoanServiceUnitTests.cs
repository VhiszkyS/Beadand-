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

public class LoanServiceUnitTests
{
    private readonly Mock<ILogger<LoanService>> _loggerMock;
    private readonly DbContextOptions<LoanContext> _dbContextOptions;
    private readonly LoanContext _context;
    private readonly LoanService _service;

    public LoanServiceUnitTests()
    {
        // Mock logger
        _loggerMock = new Mock<ILogger<LoanService>>();

        // InMemory database setup
        _dbContextOptions = new DbContextOptionsBuilder<LoanContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new LoanContext(_dbContextOptions);

        // LoanService instance
        _service = new LoanService(_loggerMock.Object, _context);
    }

    [Fact]
    public async Task AddAsync_Should_Add_Loan_To_Database()
    {
        // Arrange
        var loan = new Loan
        {
            Id = Guid.NewGuid(),
            LoanDate = DateTime.Now,
            Deadline = DateTime.Now.AddDays(7)
        };

        // Act
        await _service.AddAsync(loan);

        // Assert
        var result = await _context.Loans.FindAsync(loan.Id);
        Assert.NotNull(result);
        Assert.Equal(loan.LoanDate, result.LoanDate);
    }

    [Fact]
    public async Task DeleteAsync_Should_Remove_Loan_From_Database()
    {
        // Arrange
        var loan = new Loan
        {
            Id = Guid.NewGuid(),
            LoanDate = DateTime.Now,
            Deadline = DateTime.Now.AddDays(7)
        };

        _context.Loans.Add(loan);
        await _context.SaveChangesAsync();

        // Act
        await _service.DeleteAsync(loan.Id);

        // Assert
        var result = await _context.Loans.FindAsync(loan.Id);
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAsync_Should_Return_Loan_If_Exists()
    {
        // Arrange
        var loan = new Loan
        {
            Id = Guid.NewGuid(),
            LoanDate = DateTime.Now,
            Deadline = DateTime.Now.AddDays(7)
        };

        _context.Loans.Add(loan);
        await _context.SaveChangesAsync();

        // Act
        var result = await _service.GetAsync(loan.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(loan.Id, result.Id);
    }

    [Fact]
    public async Task GetAllAsync_Should_Return_All_Loans()
    {
        // Arrange
        var loan1 = new Loan { Id = Guid.NewGuid(), LoanDate = DateTime.Now, Deadline = DateTime.Now.AddDays(7) };
        var loan2 = new Loan { Id = Guid.NewGuid(), LoanDate = DateTime.Now, Deadline = DateTime.Now.AddDays(14) };

        _context.Loans.AddRange(loan1, loan2);
        await _context.SaveChangesAsync();

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task UpdateAsync_Should_Update_Loan_Details()
    {
        // Arrange
        var loan = new Loan
        {
            Id = Guid.NewGuid(),
            LoanDate = DateTime.Now,
            Deadline = DateTime.Now.AddDays(7)
        };

        _context.Loans.Add(loan);
        await _context.SaveChangesAsync();

        var updatedLoan = new Loan
        {
            Id = loan.Id,
            LoanDate = DateTime.Now.AddDays(-1),
            Deadline = DateTime.Now.AddDays(10)
        };

        // Act
        await _service.UpdateAsync(updatedLoan);

        // Assert
        var result = await _context.Loans.FindAsync(loan.Id);
        Assert.NotNull(result);
        Assert.Equal(updatedLoan.LoanDate, result.LoanDate);
        Assert.Equal(updatedLoan.Deadline, result.Deadline);
    }
}

