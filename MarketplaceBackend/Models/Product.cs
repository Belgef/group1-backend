namespace MarketplaceBackend.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string ExtraInformation { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public string ImageURL { get; set; }
    }
}
