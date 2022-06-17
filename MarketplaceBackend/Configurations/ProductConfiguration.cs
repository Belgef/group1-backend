using MarketplaceBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bakery.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(t => t.Name)
            .HasMaxLength(255)
            .IsRequired();

        builder.HasOne(t => t.Category)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        builder.Property(t => t.Price)
               .HasPrecision(14, 2)
               .IsRequired()
               .HasDefaultValue(0.0);
    }
}
