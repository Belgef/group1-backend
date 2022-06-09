using MarketplaceBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace MarketplaceBackend.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<CartProduct> CartProducts { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderProduct> OrderProducts { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User[]
            {
                new(){ Id = 1, FirstName="Jim", LastName="Carrey", Email="admin@gmail.com", Password="12345", Role=Role.Admin },
                new(){ Id = 2, FirstName="Simon", LastName="Cowell", Email="qwerty@gmail.com", Password="55555", Role=Role.User }
            });
        }
    }
}
