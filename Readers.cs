using System.ComponentModel.DataAnnotations;

namespace Beadandó
{
    public class Readers
    {
        public Guid id { get; set; }

        public string name { get; set; }

        public string address { get; set; }

        [Range(typeof(DateTime), "1900-01-01", "2100-12-31", ErrorMessage = "A születési dátum nem lehet kisebb mint 1900.")]
        public DateOnly BirthDate { get; set; }


    }
}
