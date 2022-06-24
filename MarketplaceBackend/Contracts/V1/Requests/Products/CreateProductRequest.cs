namespace MarketplaceBackend.Contracts.V1.Requests.Products
{
    public class CreateProductRequest
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        //public string DetailsTextPrimary { get; set; }

        //public string DetailsTextSecondary { get; set; }

        // public string ImageUrl { get; set; }
    }
}
