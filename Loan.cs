using System.ComponentModel.DataAnnotations;

namespace Beadandó
{
    public class Loan
    {
        public Guid ReaderId { get; set; }
        public Guid BookId { get; set; }

        [DataType(DataType.Date)]
        [CustomValidation(typeof(LoanValidator), nameof(LoanValidator.ValidateLoanDate))]
        public DateOnly LoanDate { get; set; }

        [CustomValidation(typeof(LoanValidator), nameof(LoanValidator.ValidateDeadline))]
        public DateOnly Deadline { get; set; }

        public class LoanValidator
        {
            public ValidationResult ValidateLoanDate(DateTime loanDate, ValidationContext context)
            {
                if (loanDate < DateTime.Today)
                {
                    return new ValidationResult("A kölcsönzés ideje nem lehet a jelenlegi napnál korábbi.");
                }
                return ValidationResult.Success;
            }

            public ValidationResult ValidateDeadline(DateOnly deadline, ValidationContext context)
            {
                var loan = (Loan)context.ObjectInstance;
                if (deadline <= loan.LoanDate)
                {
                    return new ValidationResult("A visszahozás ideje később kell legyen, mint a kikölcsönzés ideje.");
                }
                return ValidationResult.Success;
            }
        }

    }
}
