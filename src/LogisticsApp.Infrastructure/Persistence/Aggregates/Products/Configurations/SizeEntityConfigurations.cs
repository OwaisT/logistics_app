using LogisticsApp.Infrastructure.Persistence.Aggregates.Products.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LogisticsApp.Infrastructure.Persistence.Aggregates.Products.Configurations;

public class SizeEntityConfiguration : IEntityTypeConfiguration<SizeEntity>
{
    public void Configure(EntityTypeBuilder<SizeEntity> builder)
    {
        builder.ToTable("Sizes");

        builder.HasKey(size => size.Id);
        builder.Property(size => size.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(size => size.Name).IsUnique(); // avoid duplicates
    }
}
