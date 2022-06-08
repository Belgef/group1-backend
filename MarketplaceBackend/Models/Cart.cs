namespace MarketplaceBackend.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public IList<CartProduct> CartProducts { get; private set; } = new List<CartProduct>();
    }
}
