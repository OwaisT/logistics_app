using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.QCItemAggregate;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.QCItemAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LogisticsApp.Infrastructure.Persistence.Aggregates.QCItems.Configurations;

public class QCItemConfigurations : IEntityTypeConfiguration<QCItem>
{
    public void Configure(EntityTypeBuilder<QCItem> builder)
    {
        ConfigureQCItem(builder);
    }

    private static void ConfigureQCItem(EntityTypeBuilder<QCItem> builder)
    {
        builder.ToTable("QCItems");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedOnAdd()
            .HasConversion(
                id => id.Value,
                value => QCItemId.Create(value));

        builder.Property(c => c.Status)
            .HasColumnName("Status")
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(c => c.SourceName)
            .HasColumnName("SourceName")
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.SourceId)
            .HasColumnName("SourceId")
            .IsRequired();

        builder.Property(c => c.SourceItemId)
            .HasColumnName("SourceItemId")
            .IsRequired();

        builder.Property(c => c.ProductId)
            .HasColumnName("ProductId")
            .IsRequired()
            .HasConversion(
                id => id.Value,
                value => ProductId.Create(value));

        builder.Property(c => c.VariationId)
            .HasColumnName("VariationId")
            .IsRequired()
            .HasConversion(
                id => id.Value,
                value => VariationId.Create(value));

    }

}