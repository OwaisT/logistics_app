using LogisticsApp.Domain.Aggregates.Product;

namespace LogisticsApp.Application.Common.Interfaces.Persistence;

public interface IProductRepository
{
    // Define methods for product data access, e.g.:
    // Task<Product> GetByIdAsync(string id);
    // Task SaveAsync(Product product);
    void Add(Product product);
}