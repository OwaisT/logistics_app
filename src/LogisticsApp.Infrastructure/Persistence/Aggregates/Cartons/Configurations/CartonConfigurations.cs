using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Carton;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Carton.ValueObjects;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Warehouse.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LogisticsApp.Infrastructure.Persistence.Aggregates.Cartons.Configurations;

public class CartonConfigurations : IEntityTypeConfiguration<Carton>
{
    public void Configure(EntityTypeBuilder<Carton> builder)
    {
        ConfigureCarton(builder);
        ConfigureLocation(builder);
        ConfigureItems(builder);
    }

    private static void ConfigureCarton(EntityTypeBuilder<Carton> builder)
    {
        builder.ToTable("Cartons");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedOnAdd()
            .HasConversion(
                id => id.Value,
                value => CartonId.Create(value));
    }

    private static void ConfigureLocation(EntityTypeBuilder<Carton> builder)
    {
        builder.OwnsOne(c => c.Location, loc =>
        {
            loc.Property(l => l.WarehouseId)
                .HasColumnName("WarehouseId")
                .HasConversion(
                    wid => wid.Value,
                    value => WarehouseId.Create(value));

            loc.Property(l => l.WarehouseName)
                .HasColumnName("WarehouseName")
                .IsRequired()
                .HasMaxLength(200);

            loc.Property(l => l.RoomId)
                .HasColumnName("RoomId")
                .HasConversion(
                    rid => rid.Value,
                    value => RoomId.Create(value));

            loc.Property(l => l.RoomName)
                .HasColumnName("RoomName")
                .IsRequired()
                .HasMaxLength(200);

            loc.Property(l => l.OnLeft)
                .HasColumnName("OnLeft")
                .IsRequired();

            loc.Property(l => l.Below)
                .HasColumnName("Below")
                .IsRequired();

            loc.Property(l => l.Behind)
                .HasColumnName("Behind")
                .IsRequired();

            loc.HasIndex(l => new { l.WarehouseId, l.RoomId, l.OnLeft, l.Below, l.Behind })
                .IsUnique();
        });
    }

    private static void ConfigureItems(EntityTypeBuilder<Carton> builder)
    {
        builder.OwnsMany(c => c.Items, item =>
        {
            item.WithOwner().HasForeignKey("CartonId");
            item.Property(i => i.ProductId)
                .HasColumnName("ProductId")
                .HasConversion(
                    id => id.Value,
                    value => ProductId.Create(value));
            item.Property(i => i.VariationId)
                .HasColumnName("VariationId")
                .HasConversion(
                    id => id.Value,
                    value => VariationId.Create(value));
            item.Property(i => i.RefCode)
                .HasColumnName("RefCode")
                .IsRequired()
                .HasMaxLength(100);
            item.Property(i => i.Quantity)
                .HasColumnName("Quantity")
                .IsRequired();
        });
    }
}