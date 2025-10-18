using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.DefectiveItemAggregate;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.DefectiveItemAggregate.ValueObjects;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LogisticsApp.Infrastructure.Persistence.Aggregates.DefectiveItems.Configurations;

public class DefectiveItemConfiguration : IEntityTypeConfiguration<DefectiveItem>
{
    public void Configure(EntityTypeBuilder<DefectiveItem> builder)
    {
        builder.ToTable("DefectiveItems");

        builder.HasKey(di => di.Id);

        builder.Property(di => di.Id)
            .ValueGeneratedOnAdd()
            .HasConversion(
                id => id.Value,
                value => DefectiveItemId.Create(value));

        builder.Property(di => di.ProductId)
            .HasConversion(
                id => id.Value,
                value => ProductId.Create(value));

        builder.Property(di => di.VariationId)
            .HasConversion(
                id => id.Value,
                value => VariationId.Create(value));

        builder.Property(di => di.RefCode)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(di => di.Reason)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(di => di.IsRepairable)
            .IsRequired();

        builder.Property(di => di.ReportedAt)
            .IsRequired();

        builder.Property(di => di.RepairedAt);
    }
}