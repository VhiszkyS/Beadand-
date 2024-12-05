using Beadandó.Shared;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beadandó.Tests
{
    public class LoanUnitTests
    {
        [Fact]
        public void LoanDate_ShouldBeInvalid_WhenDateIsInThePast()
        {
            // Arrange
            var loan = new Loan
            {
                LoanDate = DateTime.Today.AddDays(-1), // Tegnapi dátum
                Deadline = DateTime.Today.AddDays(1) // Holnapi dátum
            };

            // Act
            var validationContext = new ValidationContext(loan);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(loan, validationContext, validationResults, true);

            // Assert
            isValid.Should().BeFalse();
            validationResults.Should().ContainSingle(result => result.ErrorMessage == "A kölcsönzés ideje nem lehet a jelenlegi napnál korábbi.");
        }

        [Fact]
        public void Deadline_ShouldBeInvalid_WhenDeadlineIsBeforeLoanDate()
        {
            // Arrange
            var loan = new Loan
            {
                LoanDate = DateTime.Today.AddDays(1), // Holnapi dátum
                Deadline = DateTime.Today // Mai dátum
            };

            // Act
            var validationContext = new ValidationContext(loan);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(loan, validationContext, validationResults, true);

            // Assert
            isValid.Should().BeFalse();
            validationResults.Should().ContainSingle(result => result.ErrorMessage == "A visszahozás ideje később kell legyen, mint a kikölcsönzés ideje.");
        }

        [Fact]
        public void Loan_ShouldBeValid_WhenLoanDateIsTodayAndDeadlineIsInTheFuture()
        {
            // Arrange
            var loan = new Loan
            {
                LoanDate = DateTime.Today, // Mai dátum
                Deadline = DateTime.Today.AddDays(1) // Holnapi dátum
            };

            // Act
            var validationContext = new ValidationContext(loan);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(loan, validationContext, validationResults, true);

            // Assert
            isValid.Should().BeTrue();
            validationResults.Should().BeEmpty();
        }

    }
}
