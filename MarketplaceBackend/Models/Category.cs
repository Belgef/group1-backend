namespace MarketplaceBackend.Models
{
    public class Category
    {
        public int Id { get; set; }

        string Name { get; set; }

        // M2M
        public string ImageURL { get; set; }

    }
}
