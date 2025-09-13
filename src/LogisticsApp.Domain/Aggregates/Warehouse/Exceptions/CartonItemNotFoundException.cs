namespace LogisticsApp.Domain.Aggregates.Warehouse.Exceptions;

using LogisticsApp.Domain.Aggregates.Product.ValueObjects;
using LogisticsApp.Domain.Common.Exceptions;

public class CartonItemNotFoundException : DomainException
{
    public CartonItemNotFoundException(VariationId variationId) : base($"Carton item with VariationId {variationId} not found.")
    {
    }
}
