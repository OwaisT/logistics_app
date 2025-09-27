using System.Text.Json;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Product;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Product.ValueObjects;
using LogisticsApp.Infrastructure.Persistence.Products.Entities;
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
        ConfigureProductSizesTable(builder);
        ConfigureProductColorsTable(builder);
    }

    private static void ConfigureProductsTable(EntityTypeBuilder<Product> builder)
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
            .HasMaxLength(6);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(p => p.GeneralPrice)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(p => p.CreatedAt)
            .IsRequired();

        builder.Property(p => p.UpdatedAt)
            .IsRequired();

        builder.Property(p => p.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(p => p.Assortments)
            .HasColumnType("jsonb")
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => JsonSerializer.Deserialize<List<Assortment>>(v, (JsonSerializerOptions?)null)!);
    }

    public static void ConfigureVariationsTable(EntityTypeBuilder<Product> builder)
    {
        builder.OwnsMany(p => p.Variations, vb =>
        {
            vb.ToTable("ProductVariations");
            vb.WithOwner().HasForeignKey("ProductId");

            vb.HasKey("Id", "ProductId");

            vb.Property(v => v.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => VariationId.Create(value));

            vb.Property(v => v.ProductRefCode)
                .IsRequired()
                .HasMaxLength(50);

            vb.Property(v => v.ProductSeason)
                .IsRequired()
                .HasMaxLength(6);

            vb.Property(v => v.VariationRefCode)
                .IsRequired()
                .HasMaxLength(50);

            vb.Property(v => v.Name)
                .IsRequired()
                .HasMaxLength(50);

            vb.Property(v => v.Description)
                .IsRequired()
                .HasMaxLength(500);

            vb.Property(v => v.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            vb.Property(v => v.Color)
                .IsRequired()
                .HasMaxLength(50);

            vb.Property(v => v.Size)
                .IsRequired()
                .HasMaxLength(20);

            vb.Property(v => v.Received)
                .IsRequired();

            vb.Property(v => v.Sold)
                .IsRequired()
                .HasDefaultValue(0);

            vb.Property(v => v.Available)
                .IsRequired();

            vb.Property(v => v.Returned)
                .IsRequired();

            vb.Property(v => v.Defective)
                .IsRequired();

            vb.Property(v => v.CreatedAt)
                .IsRequired();

            vb.Property(v => v.UpdatedAt)
                .IsRequired();
        });

        builder.Metadata
            .FindNavigation(nameof(Product.Variations))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    public static void ConfigureProductCategoriesTable(EntityTypeBuilder<Product> builder)
    {
        builder.Ignore(p => p.Categories);

        builder
            .HasMany<CategoryEntity>()
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "ProductCategories",
                j => j
                    .HasOne<CategoryEntity>()
                    .WithMany()
                    .HasForeignKey("CategoryId")
                    .HasConstraintName("FK_ProductCategories_Categories_CategoryId")
                    .OnDelete(DeleteBehavior.Cascade),
                j => j
                    .HasOne<Product>()
                    .WithMany()
                    .HasForeignKey("ProductId")
                    .HasConstraintName("FK_ProductCategories_Products_ProductId")
                    .OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.ToTable("ProductCategories");
                    j.HasKey("ProductId", "CategoryId");
                });
    }

    public static void ConfigureProductSizesTable(EntityTypeBuilder<Product> builder)
    {
        builder.Ignore(p => p.Sizes);

        builder
            .HasMany<SizeEntity>()
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "ProductSizes",
                j => j
                    .HasOne<SizeEntity>()
                    .WithMany()
                    .HasForeignKey("SizeId")
                    .HasConstraintName("FK_ProductSizes_Sizes_SizeId")
                    .OnDelete(DeleteBehavior.Cascade),
                j => j
                    .HasOne<Product>()
                    .WithMany()
                    .HasForeignKey("ProductId")
                    .HasConstraintName("FK_ProductSizes_Products_ProductId")
                    .OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.ToTable("ProductSizes");
                    j.HasKey("ProductId", "SizeId");
                });
    }

    public static void ConfigureProductColorsTable(EntityTypeBuilder<Product> builder)
    {
        builder.Ignore(p => p.Colors);

        builder
            .HasMany<ColorEntity>()
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "ProductColors",
                j => j
                    .HasOne<ColorEntity>()
                    .WithMany()
                    .HasForeignKey("ColorId")
                    .HasConstraintName("FK_ProductColors_Colors_ColorId")
                    .OnDelete(DeleteBehavior.Cascade),
                j => j
                    .HasOne<Product>()
                    .WithMany()
                    .HasForeignKey("ProductId")
                    .HasConstraintName("FK_ProductColors_Products_ProductId")
                    .OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.ToTable("ProductColors");
                    j.HasKey("ProductId", "ColorId");
                });
    }
}