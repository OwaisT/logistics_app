using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;
using LogisticsApp.Domain.Common.Models;

namespace LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.Entities;

public sealed class Variation : Entity<VariationId>
{
    private int _availableStock => Received - Sold + Returned - Defective;
    private string _variationRefCode => $"{ProductRefCode}-{Color}-{Size}";
    public string ProductRefCode { get; private set; }
    public string ProductSeason { get; private set; }
    public string VariationRefCode { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public string Color { get; private set; }
    public string Size { get; private set; }
    public int Received { get; private set; }
    public int Sold { get; private set; }
    public int Available { get; private set; }
    public int Returned { get; private set; }
    public int Defective { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private Variation(
        VariationId id,
        string productRefCode,
        string productSeason,
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
        ProductSeason = productSeason;
        Name = name;
        Description = description;
        Price = price;
        Color = color;
        Size = size;
        VariationRefCode = _variationRefCode;
        Received = 0;
        Sold = 0;
        Returned = 0;
        Defective = 0;
        Available = _availableStock;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static Variation Create(
        string productRefCode,
        string productSeason,
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
            productSeason,
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

    public void ModifyReceived(int quantity)
    {
        Received = quantity;
        Available = _availableStock;
        UpdatedAt = DateTime.UtcNow;
    }

    public void RecordSale(int quantity)
    {
        Sold += quantity;
        UpdatedAt = DateTime.UtcNow;
    }

#pragma warning disable CS8618
    private Variation() : base(default!) { }
#pragma warning restore CS8618

}