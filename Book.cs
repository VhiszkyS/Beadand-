using System.ComponentModel.DataAnnotations;

namespace Beadandó
{
    public class Book
    {
        public Guid Id {  get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Publisher { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "A kiadás éve nem negatív.")]
        public int PublishDate { get; set; }
        
        
    }
}
