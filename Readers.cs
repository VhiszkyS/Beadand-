using System.ComponentModel.DataAnnotations;

namespace Beadandó
{
    public class Reader
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        [Range(typeof(DateOnly), "1900-01-01", "2100-12-31", ErrorMessage = "A születési dátum nem lehet kisebb mint 1900.")]
        public DateOnly BirthDate { get; set; }

       
    }
}
