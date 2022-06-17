using MarketplaceBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bakery.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(t => t.FirstName)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(t => t.LastName)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(t => t.Email)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(t => t.Hash)
            .HasMaxLength(255)
            .IsRequired();
    }
}
