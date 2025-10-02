using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderAggregate.ValueObjects;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderReturnAggregate;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderReturnAggregate.ValueObjects;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LogisticsApp.Infrastructure.Persistence.Aggregates.OrderReturns.Configurations;

public class OrderReturnConfigurations : IEntityTypeConfiguration<OrderReturn>
{
    public void Configure(EntityTypeBuilder<OrderReturn> builder)
    {
        ConfigureOrderReturn(builder);
        ConfigureOrderReturnItems(builder);
    }

    private static void ConfigureOrderReturn(EntityTypeBuilder<OrderReturn> builder)
    {
        builder.ToTable("OrderReturns");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedOnAdd()
            .HasConversion(
                id => id.Value,
                value => OrderReturnId.Create(value));

        builder.Property(c => c.OrderId)
            .IsRequired()
            .HasConversion(
                id => id.Value,
                value => OrderId.Create(value));

        builder.Property(c => c.Status)
            .IsRequired();

        builder.Metadata
            .FindNavigation(nameof(OrderReturn.Items))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private static void ConfigureOrderReturnItems(EntityTypeBuilder<OrderReturn> builder)
    {
        builder.OwnsMany(c => c.Items, ib =>
        {
            ib.ToTable("OrderReturnItems");
            ib.WithOwner().HasForeignKey("OrderReturnId");
            ib.HasKey("Id", "OrderReturnId");

            ib.Property(i => i.Id)
                .ValueGeneratedNever()
                .HasColumnName("Id")
                .HasConversion(
                    id => id.Value,
                    value => OrderReturnItemId.Create(value));

            ib.Property(i => i.OrderItemId)
                .IsRequired()
                .HasConversion(
                    id => id.Value,
                    value => OrderItemId.Create(value));

            ib.Property(i => i.ProductId)
                .IsRequired()
                .HasConversion(
                    id => id.Value,
                    value => ProductId.Create(value));

            ib.Property(i => i.VariationId)
                .IsRequired()
                .HasConversion(
                    id => id.Value,
                    value => VariationId.Create(value));

            ib.Property(i => i.RefCode)
                .IsRequired();
        });
    }
}