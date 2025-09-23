using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Product;
using LogisticsApp.Infrastructure.Persistence.Products.Models;
using Microsoft.EntityFrameworkCore;

namespace LogisticsApp.Infrastructure.Persistence.Products.Helpers;

public class DBInsertionHelper
{
    // This class can be used for common DB insertion logic if needed in the future.
    private readonly DbContext _dbContext;

    public DBInsertionHelper(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void CreateAndInsertProductSizes(Product product, List<Size> sizes)
    {
        foreach (var size in sizes)
        {
            if (size.Id == Guid.Empty)
                _dbContext.Set<Size>().Add(size);
            else
                _dbContext.Set<Size>().Attach(size);
        }
        var productSizes = sizes.Select(size => new Dictionary<string, object>
        {
            ["ProductId"] = product.Id,
            ["SizeId"] = size.Id
        }).ToList();
        foreach (var entry in productSizes)
        {
            _dbContext.Set<Dictionary<string, object>>("ProductSizes").Add(entry);
        }
    }

    public void CreateAndInsertProductColors(Product product, List<Color> colors)
    {
        foreach (var color in colors)
        {
            if (color.Id == Guid.Empty)
                _dbContext.Set<Color>().Add(color);
            else
                _dbContext.Set<Color>().Attach(color);
        }
        var productColors = colors.Select(color => new Dictionary<string, object>
        {
            ["ProductId"] = product.Id,
            ["ColorId"] = color.Id
        }).ToList();
        foreach (var entry in productColors)
        {
            _dbContext.Set<Dictionary<string, object>>("ProductColors").Add(entry);
        }
    }

    public void CreateAndInsertProductCategories(Product product, List<Category> categories)
    {
        foreach (var category in categories)
        {
            if (category.Id == Guid.Empty)
                _dbContext.Set<Category>().Add(category);
            else
                _dbContext.Set<Category>().Attach(category);
        }
        var productCategories = categories.Select(category => new Dictionary<string, object>
        {
            ["ProductId"] = product.Id,
            ["CategoryId"] = category.Id
        }).ToList();
        foreach (var entry in productCategories)
        {
            _dbContext.Set<Dictionary<string, object>>("ProductCategories").Add(entry);
        }
    }

}
