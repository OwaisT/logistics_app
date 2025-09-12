using LogisticsApp.Domain.Aggregates.Product.ValueObjects;
using LogisticsApp.Domain.Common.Models;

namespace LogisticsApp.Domain.Aggregates.Product.Entities;

public sealed class Variation : Entity<VariationId>
{
    public string ProductRefCode { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public string Color { get; private set; }
    public string Size { get; private set; }
    public int Received { get; private set; }
    public int Sold { get; private set; }
    public int Available => Received - Sold + Returned - Defective;
    public int Returned { get; private set; }
    public int Defective { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private Variation(
        VariationId id,
        string productRefCode,
        string name,
        string description,
        decimal price,
        string color,
        string size,
        DateTime createdAt,
        DateTime updatedAt)
        : base(id)
    {
        ProductRefCode = productRefCode;
        Name = name;
        Description = description;
        Price = price;
        Color = color;
        Size = size;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static Variation Create(
        string productRefCode,
        string name,
        string description,
        decimal price,
        string color,
        string size)
    {
        var variationId = VariationId.CreateUnique();
        return new(
            variationId,
            productRefCode,
            name,
            description,
            price,
            color,
            size,
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

    private Variation() : base(default!) { }


}