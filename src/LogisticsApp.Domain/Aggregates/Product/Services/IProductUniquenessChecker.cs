namespace LogisticsApp.Domain.Aggregates.Product.Services;

public interface IProductUniquenessChecker
{
    bool IsUnique(string refCode, string season);
}
