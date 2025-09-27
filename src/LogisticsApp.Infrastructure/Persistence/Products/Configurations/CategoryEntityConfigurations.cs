using LogisticsApp.Infrastructure.Persistence.Products.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LogisticsApp.Infrastructure.Persistence.Products.Configurations;

public class CategoryEntityConfiguration : IEntityTypeConfiguration<CategoryEntity>
{
    public void Configure(EntityTypeBuilder<CategoryEntity> builder)
    {
        builder.ToTable("Categories");

        builder.HasKey(category => category.Id);
        builder.Property(category => category.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(category => category.Name).IsUnique(); // avoid duplicates
    }
}
