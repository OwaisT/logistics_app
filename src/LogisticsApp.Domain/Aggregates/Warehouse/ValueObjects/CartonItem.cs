using LogisticsApp.Domain.Aggregates.Product.ValueObjects;
using LogisticsApp.Domain.Common.Exceptions;
using LogisticsApp.Domain.Common.Models;

namespace LogisticsApp.Domain.Aggregates.Warehouse.ValueObjects;

public sealed class CartonItem : ValueObject
{
    public Guid Id { get; private set; }
    public VariationId VariationId { get; private set; }
    public int Quantity { get; private set; }
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; private set; }

    private CartonItem(
        Guid id,
        VariationId variationId,
        int quantity)
    {
        Id = id;
        VariationId = variationId;
        Quantity = quantity;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public static CartonItem Create(VariationId variationId, int quantity)
    {
        var id = Guid.NewGuid();
        return new CartonItem(id, variationId, quantity);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id;
        yield return VariationId;
        yield return Quantity;
    }

    public void IncreaseQuantity(int quantity)
    {
        Quantity += quantity;
        UpdatedAt = DateTime.UtcNow;
    }

    public void DecreaseQuantity(int quantity)
    {
        if (Quantity - quantity < 0)
        {
            throw new CannotBeNegativeException(nameof(Quantity));
        }
        Quantity -= quantity;
        UpdatedAt = DateTime.UtcNow;
    }
}
