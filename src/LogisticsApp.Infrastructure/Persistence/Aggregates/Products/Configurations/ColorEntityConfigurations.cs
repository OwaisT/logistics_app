using LogisticsApp.Infrastructure.Persistence.Aggregates.Products.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LogisticsApp.Infrastructure.Persistence.Aggregates.Products.Configurations;

public class ColorEntityConfiguration : IEntityTypeConfiguration<ColorEntity>
{
    public void Configure(EntityTypeBuilder<ColorEntity> builder)
    {
        builder.ToTable("Colors");
        builder.HasKey(color => color.Id);
        builder.Property(color => color.Name)
            .IsRequired()
            .HasMaxLength(100);
        builder.HasIndex(color => color.Name).IsUnique(); // avoid duplicates
    }
}
