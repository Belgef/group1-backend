namespace MarketplaceBackend.Contracts.V1.Requests.Products
{
    public class GetAllProductsQuery
    {
        public string Search { get; set; } = string.Empty;

        public int CategoryId { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}
