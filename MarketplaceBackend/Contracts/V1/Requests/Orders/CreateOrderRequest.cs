namespace MarketplaceBackend.Contracts.V1.Requests.Orders
{
    public class CreateOrderRequest
    {
        public IList<OrderProductDto> OrderProducts { get; set; } = new List<OrderProductDto>();
    }
}
