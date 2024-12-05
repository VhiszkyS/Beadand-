using System.ComponentModel.DataAnnotations;
using Beadandó.Shared;
using Beadandó;
using Xunit;
using System;

namespace Beadandó.Tests;

public class BookUnitTests
{
    [Fact]
    public void Book_ValidTitle_ShouldNotHaveValidationErrors()
    {
        // Arrange
        var book = new Book
        {
            Title = "Valid Book Title",
            Author = "Author Name",
            Publisher = "Publisher Name",
            PublishDate = 2024
        };

        // Act
        var validationResults = ValidateModel(book);

        // Assert
        Assert.Empty(validationResults);
    }

    [Fact]
    public void Book_EmptyTitle_ShouldHaveValidationError()
    {
        // Arrange
        var book = new Book
        {
            Title = string.Empty,  // Invalid Title
            Author = "Author Name",
            Publisher = "Publisher Name",
            PublishDate = 2024
        };

        // Act
        var validationResults = ValidateModel(book);

        // Assert
        Assert.Single(validationResults);
        Assert.Equal("The Title field is required.", validationResults[0].ErrorMessage);
    }

    [Fact]
    public void Book_NegativePublishDate_ShouldHaveValidationError()
    {
        // Arrange
        var book = new Book
        {
            Title = "Valid Book Title",
            Author = "Author Name",
            Publisher = "Publisher Name",
            PublishDate = -1  // Invalid PublishDate
        };

        // Act
        var validationResults = ValidateModel(book);

        // Assert
        Assert.Single(validationResults);
        Assert.Equal("A kiadás éve nem lehet negatív.", validationResults[0].ErrorMessage);
    }

    [Fact]
    public void Book_ValidPublishDate_ShouldNotHaveValidationErrors()
    {
        // Arrange
        var book = new Book
        {
            Title = "Valid Book Title",
            Author = "Author Name",
            Publisher = "Publisher Name",
            PublishDate = 2024  // Valid PublishDate
        };

        // Act
        var validationResults = ValidateModel(book);

        // Assert
        Assert.Empty(validationResults);
    }

    private static IList<ValidationResult> ValidateModel(Book book)
    {
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(book);
        Validator.TryValidateObject(book, validationContext, validationResults, true);
        return validationResults;
    }
}

