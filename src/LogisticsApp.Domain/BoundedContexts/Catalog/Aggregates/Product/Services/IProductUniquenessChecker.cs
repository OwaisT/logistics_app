namespace LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Product.Services;

public interface IProductUniquenessChecker
{
    bool IsUnique(string refCode, string season);
}
