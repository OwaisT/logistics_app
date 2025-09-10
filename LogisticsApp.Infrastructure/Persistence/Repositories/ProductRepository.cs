using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.Aggregates.Product;

namespace LogisticsApp.Infrastructure.Persistence.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly LogisticsAppDbContext _dbContext;
    private static readonly List<Product> _products = [];

    public ProductRepository(LogisticsAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(Product product)
    {
        _products.Add(product);
        // _dbContext.Add(product);
        // _dbContext.SaveChanges();
    }
}
