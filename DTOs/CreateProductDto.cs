using System.ComponentModel.DataAnnotations;

namespace StorageAPI.DTO
{
    public class CreateProductDto
    {
        public CreateProductDto(string name, int price, string category, string shelf, int count, string description)
        {
            Name = name;
            Price = price;
            Category = category;
            Shelf = shelf;
            Count = count;
            Description = description;
        }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range (0, 100000)]
        public int Price { get; set; }

        public string Category { get; set; } = string.Empty;

        [Required]
        public string Shelf { get; set; } = string.Empty;

        [Required]
        public int Count { get; set; }

        [Required]
        [StringLength(40)]
        public string Description { get; set; } = string.Empty;
    }
}
