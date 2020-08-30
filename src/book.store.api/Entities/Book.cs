using System.ComponentModel.DataAnnotations.Schema;

namespace book.store.api.Entities
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
