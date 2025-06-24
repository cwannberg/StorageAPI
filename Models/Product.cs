namespace StorageAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Category { get; set; }
        public string Shelf { get; set; }
        public int Count { get; set; }
        public string Description { get; set; }

        public Product(int id, string name, int price, string category, string shelf, int count, string description)
        {
            Id = id;
            Name = name;
            Price = price;
            Category = category;
            Shelf = shelf;
            Count = count;
            Description = description;
        }
    }
}
