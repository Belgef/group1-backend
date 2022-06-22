namespace MarketplaceBackend.Models
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime OnDate { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public IList<OrderProduct> OrderProducts { get; private set; } = new List<OrderProduct>();
    }
}
