using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Product;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Product.ValueObjects;
using LogisticsApp.Infrastructure.Persistence.Products.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LogisticsApp.Infrastructure.Persistence.Products.Configurations;

public class ProductConfigurations : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        ConfigureProductsTable(builder);
        ConfigureVariationsTable(builder);
        ConfigureProductCategoriesTable(builder);
        ConfigureProductColorsTable(builder);
        ConfigureProductSizesTable(builder);
    }

    private void ConfigureVariationsTable(EntityTypeBuilder<Product> builder)
    {
        builder.OwnsMany(p => p.Variations, vb =>
        {
            vb.ToTable("ProductVariations");
            vb.WithOwner().HasForeignKey("ProductId");
            vb.Property(v => v.Id)
                .HasColumnName("VariationId")
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => VariationId.Create(value));
            vb.HasKey("Id", "ProductId");
            vb.Property(v => v.ProductRefCode)
                .IsRequired()
                .HasMaxLength(50);
            vb.Property(v => v.Name)
                .IsRequired()
                .HasMaxLength(100);
            vb.Property(v => v.Description)
                .HasMaxLength(500);
            vb.Property(v => v.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
            vb.Property(v => v.Color)
                .IsRequired()
                .HasMaxLength(50);
            vb.Property(v => v.Size)
                .IsRequired()
                .HasMaxLength(50);
            vb.Property(v => v.Received)
                .IsRequired()
                .HasDefaultValue(0);
            vb.Property(v => v.Sold)
                .IsRequired()
                .HasDefaultValue(0);
            vb.Property(v => v.Returned)
                .IsRequired()
                .HasDefaultValue(0);
            vb.Property(v => v.Defective)
                .IsRequired()
                .HasDefaultValue(0);
            // vb.Property(v => v.Available)
            //     .IsRequired();
            // vb.Property(v => v.CreatedAt)
            //     .IsRequired();
            // vb.Property(v => v.UpdatedAt)
            //     .IsRequired();

        });

        builder.Metadata.FindNavigation(nameof(Product.Variations))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureProductSizesTable(EntityTypeBuilder<Product> builder)
    {
        builder
            .Ignore(p => p.Sizes); // EF doesn’t persist domain names directly

        builder
            .HasMany<Size>() // use EF Size entity
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "ProductSizes",
                j => j
                    .HasOne<Size>()
                    .WithMany()
                    .HasForeignKey("SizeId")
                    .OnDelete(DeleteBehavior.Cascade),
                j => j
                    .HasOne<Product>()
                    .WithMany()
                    .HasForeignKey("ProductId")
                    .OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.ToTable("ProductSizes");
                    j.HasKey("ProductId", "SizeId");
                });
    }

    private void ConfigureProductColorsTable(EntityTypeBuilder<Product> builder)
    {
        builder
            .Ignore(p => p.Colors); // EF doesn’t persist domain names directly

        builder
            .HasMany<Color>() // use EF Color entity
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "ProductColors",
                j => j
                    .HasOne<Color>()
                    .WithMany()
                    .HasForeignKey("ColorId")
                    .OnDelete(DeleteBehavior.Cascade),
                j => j
                    .HasOne<Product>()
                    .WithMany()
                    .HasForeignKey("ProductId")
                    .OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.ToTable("ProductColors");
                    j.HasKey("ProductId", "ColorId");
                });
    }

    private void ConfigureProductCategoriesTable(EntityTypeBuilder<Product> builder)
    {
        builder
            .Ignore(p => p.Categories); // EF doesn’t persist domain names directly

        builder
            .HasMany<Category>() // use EF Category entity
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "ProductCategories",
                j => j
                    .HasOne<Category>()
                    .WithMany()
                    .HasForeignKey("CategoryId")
                    .OnDelete(DeleteBehavior.Cascade),
                j => j
                    .HasOne<Product>()
                    .WithMany()
                    .HasForeignKey("ProductId")
                    .OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.ToTable("ProductCategories");
                    j.HasKey("ProductId", "CategoryId");
                });
    }


    private void ConfigureProductsTable(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => ProductId.Create(value));

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

        builder.Ignore(p => p.Assortments);

    }
}