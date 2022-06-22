using MarketplaceBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarketplaceBackend.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasOne(t => t.User)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

            builder.HasMany(t => t.OrderProducts)
            .WithOne(t => t.Order)
            .IsRequired();
        }
    }
}
