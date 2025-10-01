using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Warehouse;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Warehouse.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LogisticsApp.Infrastructure.Persistence.Aggregates.Warehouses.Configurations;

public class WarehouseConfigurations : IEntityTypeConfiguration<Warehouse>
{
    public void Configure(EntityTypeBuilder<Warehouse> builder)
    {
        ConfigureWarehousesTable(builder);
        ConfigureRoomsTable(builder);
    }

    private static void ConfigureWarehousesTable(EntityTypeBuilder<Warehouse> builder)
    {
        builder.ToTable("Warehouses");

        builder.HasKey(w => w.Id);

        builder.Property(w => w.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => WarehouseId.Create(value));

        builder.Property(w => w.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(w => w.Street)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(w => w.Area)
            .HasMaxLength(100);

        builder.Property(w => w.City)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(w => w.Postcode)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(w => w.Country)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(w => w.CreatedAt)
            .IsRequired();

        builder.Property(w => w.UpdatedAt)
            .IsRequired();

        builder.Property(w => w.IsActive)
            .IsRequired()
            .HasDefaultValue(true);
    }

    private static void ConfigureRoomsTable(EntityTypeBuilder<Warehouse> builder)
    {
        builder.OwnsMany(w => w.Rooms, rb =>
        {
            rb.ToTable("Rooms");
            rb.WithOwner().HasForeignKey("WarehouseId");

            rb.HasKey("Id", "WarehouseId");

            rb.Property(r => r.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => RoomId.Create(value));

            rb.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(100);
        });

        builder.Metadata
            .FindNavigation(nameof(Warehouse.Rooms))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
    
}