using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;

namespace LogisticsApp.Application.Common.Interfaces.Persistence;

public interface IProductRepository
{
    // Define methods for product data access, e.g.:
    // Task<Product> GetByIdAsync(string id);
    // Task SaveAsync(Product product);
    void Add(Product product);

    List<Product> GetAll();
    Task<List<Product>> GetAllAsync();

    Product? GetById(ProductId id);

    Product? GetByDetails(string refCode, string season);

    void Update(Product product);
}