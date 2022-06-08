namespace MarketplaceBackend.Models
{
    public class CartProduct
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public int CartId { get; set; }

        public Cart Cart { get; set; }

        int ProductId { get; set; }

        public Product Product { get; set; }

    }
}
