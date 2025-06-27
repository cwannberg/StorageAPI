using System.ComponentModel.DataAnnotations.Schema;

namespace StorageAPI.Models
{
    [Table ("Products")]
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public int Price { get; set; }
        public string Category { get; set; } = String.Empty;
        public string Shelf { get; set; } = String.Empty;
        public int Count { get; set; }
        public string Description { get; set; } = String.Empty;
    }
}
