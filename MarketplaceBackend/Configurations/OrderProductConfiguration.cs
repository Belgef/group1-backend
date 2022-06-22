using MarketplaceBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarketplaceBackend.Configurations
{
    public class OrderProductConfiguration : IEntityTypeConfiguration<OrderProduct>
    {
        public void Configure(EntityTypeBuilder<OrderProduct> builder)
        {
            builder.HasOne(t => t.Product)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

            builder.Property(t => t.Quantity)
               .IsRequired();

            builder.Property(t => t.UnitPrice)
               .HasPrecision(14, 2)
               .IsRequired()
               .HasDefaultValue(0.0);
        }
    }
}
