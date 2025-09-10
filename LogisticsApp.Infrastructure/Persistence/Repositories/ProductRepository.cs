using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.Aggregates.Product;

namespace LogisticsApp.Infrastructure.Persistence.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly LogisticsAppDbContext _dbContext;

    public ProductRepository(LogisticsAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(Product product)
    {
        _dbContext.Add(product);
        _dbContext.SaveChanges();
    }
}