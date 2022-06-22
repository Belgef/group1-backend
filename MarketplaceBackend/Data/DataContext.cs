using MarketplaceBackend.Helpers;
using MarketplaceBackend.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace MarketplaceBackend.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Photo> Photos { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderProduct> OrderProducts { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            byte[] salt1 = PasswordEncoder.GenerateSalt(), salt2 = PasswordEncoder.GenerateSalt();
            string hash1 = PasswordEncoder.HashPassword("A12345", salt1);
            string hash2 = PasswordEncoder.HashPassword("U55555", salt2);

            modelBuilder.Entity<User>().HasData(new User[]
            {
                new(){ Id = 1, FirstName="Jim", LastName="Carrey", Email="admin@gmail.com", Salt=Convert.ToBase64String(salt1), Hash=hash1, Role=Role.Admin },
                new(){ Id = 2, FirstName="Simon", LastName="Cowell", Email="qwerty@gmail.com", Salt=Convert.ToBase64String(salt2), Hash=hash2, Role=Role.User }
            });
        }
    }
}
