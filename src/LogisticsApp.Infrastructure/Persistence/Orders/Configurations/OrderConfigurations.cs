using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Order;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Order.ValueObjects;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Product.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LogisticsApp.Infrastructure.Persistence.Orders.Configurations;

public class OrderConfigurations : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        ConfigureOrder(builder);
        ConfigureOrderItem(builder);
    }

    private static void ConfigureOrder(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedOnAdd()
            .HasConversion(
                id => id.Value,
                value => OrderId.Create(value));

        builder.Property(c => c.TotalItemsCount)
            .IsRequired();

        builder.Property(c => c.TotalValue)
            .IsRequired();

        builder.Property(c => c.Status)
            .IsRequired();

    }
    
    private static void ConfigureOrderItem(EntityTypeBuilder<Order> builder)
    {
        builder.OwnsMany(c => c.Items, item =>
        {
            item.WithOwner().HasForeignKey("OrderId");
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
            item.Property(i => i.Status)
                .HasColumnName("Status")
                .IsRequired()
                .HasMaxLength(50);
        });

    }


}