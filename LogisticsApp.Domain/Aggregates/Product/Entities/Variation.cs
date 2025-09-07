using LogisticsApp.Domain.Aggregates.Product.ValueObjects;
using LogisticsApp.Domain.Common.Models;

namespace LogisticsApp.Domain.Aggregates.Product.Entities;

public sealed class Variation : Entity<VariationId>
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public string Color { get; private set; }
    public string Size { get; private set; }
    public int Received { get; private set; }
    public int Sold { get; private set; }
    public int Available => Received - Sold;
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private Variation(
        VariationId id,
        string name,
        string description,
        decimal price,
        DateTime createdAt,
        DateTime updatedAt)
        : base(id)
    {
        Name = name;
        Description = description;
        Price = price;
        Color = id.Color;
        Size = id.Size;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static Variation Create(
        ProductId productId,
        string name,
        string description,
        decimal price,
        string color,
        string size)
    {
        var variationId = VariationId.Create(productId, color, size);
        return new(
            variationId,
            name,
            description,
            price,
            DateTime.UtcNow,
            DateTime.UtcNow);
    }

    public void UpdatePrice(decimal newPrice)
    {
        Price = newPrice;
        UpdatedAt = DateTime.UtcNow;
    }

    public void IncreaseReceived(int quantity)
    {
        Received += quantity;
        UpdatedAt = DateTime.UtcNow;
    }

    public void RecordSale(int quantity)
    {
        Sold += quantity;
        UpdatedAt = DateTime.UtcNow;
    }


}