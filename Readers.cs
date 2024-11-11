using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beadandó
{
    public class Reader
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Address { get; set; }

        [Range(typeof(DateOnly), "1900-01-01", "2100-12-31", ErrorMessage = "A születési dátum nem lehet kisebb mint 1900.")]
        public DateOnly BirthDate { get; set; }

       
    }
}
