using LogisticsApp.Domain.Aggregates.Product;
using LogisticsApp.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LogisticsApp.Infrastructure.Persistence.Configurations.Products;

public class AssortmentEntryConfigurations : IEntityTypeConfiguration<AssortmentEntry>
{
    public void Configure(EntityTypeBuilder<AssortmentEntry> builder)
    {
        builder.ToTable("ProductAssortments");
        builder.HasKey(a => new { a.ProductId, a.Color, a.Size });

        builder.HasOne<Product>()
            .WithMany()
            .HasForeignKey(a => a.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(a => a.Color)
            .IsRequired();
        builder.Property(a => a.Size)
            .IsRequired();
        builder.Property(a => a.Quantity)
            .IsRequired();



    }
}
    