namespace MarketplaceBackend.Models
{
    public class Order
    {
        public int Id { get; set; }

        public IList<OrderProduct> OrderProducts { get; private set; } = new List<OrderProduct>();
    }
}
