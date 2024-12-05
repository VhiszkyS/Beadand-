using System;
using Beadandó.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using System.ComponentModel.DataAnnotations;

namespace Beadandó.Tests
{
    public class ReaderUnitTests
    {
        [Fact]
        public void Reader_ShouldBeInvalid_WhenBirthDateIsBefore1900()
        {
            // Arrange
            var reader = new Reader
            {
                Name = "Test Reader",
                Address = "Test Address",
                BirthDate = new DateOnly(1899, 12, 31) // 1899-es születési dátum
            };

            // Act
            var validationContext = new ValidationContext(reader);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(reader, validationContext, validationResults, true);

            // Assert
            isValid.Should().BeFalse();
            validationResults.Should().ContainSingle(result => result.ErrorMessage == "A születési dátum nem lehet kisebb mint 1900.");
        }

        [Fact]
        public void Reader_ShouldBeValid_WhenBirthDateIsValid()
        {
            // Arrange
            var reader = new Reader
            {
                Name = "Test Reader",
                Address = "Test Address",
                BirthDate = new DateOnly(2000, 1, 1) // 2000-es születési dátum
            };

            // Act
            var validationContext = new ValidationContext(reader);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(reader, validationContext, validationResults, true);

            // Assert
            isValid.Should().BeTrue();
            validationResults.Should().BeEmpty();
        }

        [Fact]
        public void Reader_ShouldBeInvalid_WhenNameIsTooLong()
        {
            // Arrange
            var reader = new Reader
            {
                Name = new string('A', 101), // 101 karakteres név
                Address = "Test Address",
                BirthDate = new DateOnly(2000, 1, 1)
            };

            // Act
            var validationContext = new ValidationContext(reader);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(reader, validationContext, validationResults, true);

            // Assert
            isValid.Should().BeFalse();
            validationResults.Should().ContainSingle(result => result.ErrorMessage == "'Name' nem lehet hosszabb, mint 100 karakter.");
        }

        [Fact]
        public void Reader_ShouldBeValid_WhenNameAndAddressAreValid()
        {
            // Arrange
            var reader = new Reader
            {
                Name = "Valid Name",
                Address = "Valid Address",
                BirthDate = new DateOnly(2000, 1, 1)
            };

            // Act
            var validationContext = new ValidationContext(reader);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(reader, validationContext, validationResults, true);

            // Assert
            isValid.Should().BeTrue();
            validationResults.Should().BeEmpty();
        }
    }
}
