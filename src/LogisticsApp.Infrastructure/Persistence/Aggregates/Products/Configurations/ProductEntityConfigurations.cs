using System.Text.Json;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;
using LogisticsApp.Infrastructure.Persistence.Aggregates.Products.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LogisticsApp.Infrastructure.Persistence.Aggregates.Products.Configurations;

public class ProductEntityConfigurations : IEntityTypeConfiguration<ProductEntity>
{
    public void Configure(EntityTypeBuilder<ProductEntity> builder)
    {
        ConfigureProductsTable(builder);
        ConfigureVariationsTable(builder);
        ConfigureProductCategoriesTable(builder);
        ConfigureProductSizesTable(builder);
        ConfigureProductColorsTable(builder);
    }

    private static void ConfigureProductsTable(EntityTypeBuilder<ProductEntity> builder)
    {
        builder.ToTable("Products");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedNever();

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

    private static void ConfigureVariationsTable(EntityTypeBuilder<ProductEntity> builder)
    {
        builder.OwnsMany(p => p.Variations, vb =>
        {
            vb.ToTable("ProductVariations");
            vb.WithOwner().HasForeignKey("ProductId");

            vb.HasKey("Id", "ProductId");

            vb.Property(v => v.Id)
                .ValueGeneratedNever();

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

            vb.Property(v => v.Repaired)
                .IsRequired();

            vb.Property(v => v.CreatedAt)
                .IsRequired();

            vb.Property(v => v.UpdatedAt)
                .IsRequired();
        });
    }

    private static void ConfigureProductCategoriesTable(EntityTypeBuilder<ProductEntity> builder)
    {
        builder
        .HasMany(p => p.Categories)
        .WithMany(c => c.Products)
        .UsingEntity<Dictionary<string, object>>(
            "ProductCategories",
            j => j
                .HasOne<CategoryEntity>()
                .WithMany()
                .HasForeignKey("CategoryId")
                .OnDelete(DeleteBehavior.Cascade),
            j => j
                .HasOne<ProductEntity>()
                .WithMany()
                .HasForeignKey("ProductId")
                .OnDelete(DeleteBehavior.Cascade),
            j =>
            {
                j.HasKey("ProductId", "CategoryId");
            });
    }

    private static void ConfigureProductSizesTable(EntityTypeBuilder<ProductEntity> builder)
    {
        builder
        .HasMany(p => p.Sizes)
        .WithMany(s => s.Products)
        .UsingEntity<Dictionary<string, object>>(
            "ProductSizes",
            j => j
                .HasOne<SizeEntity>()
                .WithMany()
                .HasForeignKey("SizeId")
                .OnDelete(DeleteBehavior.Cascade),
            j => j
                .HasOne<ProductEntity>()
                .WithMany()
                .HasForeignKey("ProductId")
                .OnDelete(DeleteBehavior.Cascade),
            j =>
            {
                j.HasKey("ProductId", "SizeId");
            });
    }

    private static void ConfigureProductColorsTable(EntityTypeBuilder<ProductEntity> builder)
    {
        builder
        .HasMany(p => p.Colors)
        .WithMany(c => c.Products)
        .UsingEntity<Dictionary<string, object>>(
            "ProductColors",
            j => j
                .HasOne<ColorEntity>()
                .WithMany()
                .HasForeignKey("ColorId")
                .OnDelete(DeleteBehavior.Cascade),
            j => j
                .HasOne<ProductEntity>()
                .WithMany()
                .HasForeignKey("ProductId")
                .OnDelete(DeleteBehavior.Cascade),
            j =>
            {
                j.HasKey("ProductId", "ColorId");
            });
    }
}