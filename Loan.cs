using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeadandóShared
{
    public class Loan
    {
        [Key]
        public Guid Id { get; set; }

        public Guid ReaderId { get; set; }

        public Guid BookId { get; set; }

        [DataType(DataType.Date)]
        [CustomValidation(typeof(LoanValidator), nameof(LoanValidator.ValidateLoanDate))]
        public DateTime LoanDate { get; set; }

        [CustomValidation(typeof(LoanValidator), nameof(LoanValidator.ValidateDeadline))]
        public DateTime Deadline { get; set; }

        public virtual ICollection<Item>? Items { get; set; }

        public class LoanValidator
        {
            public static ValidationResult ValidateLoanDate(DateTime loanDate, ValidationContext context)
            {
                if (loanDate < DateTime.Today)
                {
                    return new ValidationResult("A kölcsönzés ideje nem lehet a jelenlegi napnál korábbi.");
                }
                return ValidationResult.Success;
            }

            public static ValidationResult ValidateDeadline(DateTime deadline, ValidationContext context)
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
