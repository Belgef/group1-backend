namespace MarketplaceBackend.Contracts.V1.Requests.Orders
{
    public class OrderProductDto
    {
        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public int ProductId { get; set; }
    }
}
