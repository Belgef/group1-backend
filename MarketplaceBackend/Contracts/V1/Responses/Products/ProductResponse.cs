using MarketplaceBackend.Common.Mappings;
using MarketplaceBackend.Models;

namespace MarketplaceBackend.Contracts.V1.Responses.Products
{
    public class ProductResponse : IMapFrom<Product>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string ExtraInformation { get; set; }

        public int SoldCount { get; set; }

        public string ImageURL { get; set; }
    }
}
