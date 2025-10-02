namespace LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.Services;

public interface IProductUniquenessChecker
{
    bool IsUnique(string refCode, string season);
}
