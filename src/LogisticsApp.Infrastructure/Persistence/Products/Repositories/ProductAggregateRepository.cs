using LogisticsApp.Domain.Aggregates.Product;

namespace LogisticsApp.Infrastructure.Persistence.Products.Repositories;

public class ProductAggregateRepository : IProductAggregateRepository
{
    // private readonly LogisticsAppDbContext _dbContext;
    private static readonly List<Product> _products = [];

    public List<Product> GetAll()
    {
        return _products;
    }
}