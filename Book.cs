using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beadandó
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id {  get; set; }

        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(100)]
        public string Author { get; set; }

        [MaxLength(100)]
        public string Publisher { get; set; }

        [Range(0, 9999, ErrorMessage = "A kiadás éve nem negatív.")]
        public int PublishDate { get; set; }
        
        
    }
}
