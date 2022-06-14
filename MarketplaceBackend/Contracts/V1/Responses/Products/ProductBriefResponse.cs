using MarketplaceBackend.Common.Mappings;
using MarketplaceBackend.Models;

namespace MarketplaceBackend.Contracts.V1.Responses.Products
{
    public class ProductBriefResponse : IMapFrom<Product>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int SoldCount { get; set; }

        public string ImageURL { get; set; }
    }
}
