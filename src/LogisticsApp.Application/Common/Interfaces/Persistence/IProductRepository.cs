using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Product;

namespace LogisticsApp.Application.Common.Interfaces.Persistence;

public interface IProductRepository
{
    // Define methods for product data access, e.g.:
    // Task<Product> GetByIdAsync(string id);
    // Task SaveAsync(Product product);
    void Add(Product product);

    List<Product> GetAll();
    Task<List<Product>> GetAllAsync();

    Product? GetById(Guid id);

    Product? GetByDetails(string refCode, string season);
}