using System.ComponentModel.DataAnnotations;

namespace StorageAPI.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range (0, 100000)]
        public int Price { get; set; }

        [Required]
        public int Count { get; set; }

        public int InventoryValue(int price, int count)
        {
            return price * count;
        }
    }
}
