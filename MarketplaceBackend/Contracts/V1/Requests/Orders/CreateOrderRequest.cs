namespace MarketplaceBackend.Contracts.V1.Requests.Orders
{
    public class CreateOrderRequest
    {
        public DateTime OnDate { get; set; }

        public IList<OrderProductDto> OrderProducts { get; set; } = new List<OrderProductDto>();
    }
}
