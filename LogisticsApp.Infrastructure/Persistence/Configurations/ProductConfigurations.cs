using LogisticsApp.Domain.Aggregates.Product;
using LogisticsApp.Domain.Aggregates.Product.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LogisticsApp.Infrastructure.Persistence.Configurations;

public class ProductConfigurations : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        ConfigureMenusTable(builder);
        ConfigureCategoriesTable(builder);

    }

    private void ConfigureCategoriesTable(EntityTypeBuilder<Product> builder)
    {
        builder.OwnsMany(p => p.Categories, cb =>
        {
            cb.ToTable("ProductCategories");
            cb.WithOwner().HasForeignKey("ProductId");
            cb.Property<string>("Category")
                .HasColumnName("Category")
                .IsRequired()
                .HasMaxLength(50);
        });
    }

    private void ConfigureMenusTable(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(p => p.ProductId);

        builder.Property(p => p.ProductId)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => ProductId.CreateConversion(value));

        builder.Property(p => p.RefCode)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.Season)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Description)
            .HasMaxLength(500);

        // builder.Property(p => p.CreatedAt)
        //     .IsRequired();

        // builder.Property(p => p.UpdatedAt)
        //     .IsRequired();

        builder.Property(p => p.IsActive)
            .IsRequired()
            .HasDefaultValue(true);
        
    }
}