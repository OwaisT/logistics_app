using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Product;
using LogisticsApp.Infrastructure.Persistence.Aggregates.Products.Entities;

namespace LogisticsApp.Infrastructure.Persistence.Aggregates.Products.Helpers;

public class ProductDBInsertionHelper(
    LogisticsAppDbContext _dbContext)
{
    public void CreateAndInsertProductCategories(Product product, List<CategoryEntity> categories)
    {
        foreach (var category in categories)
        {
            if (category.Id == Guid.Empty)
                _dbContext.Set<CategoryEntity>().Add(category);
            else
                _dbContext.Set<CategoryEntity>().Attach(category);
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
    public void CreateAndInsertProductColors(Product product, List<ColorEntity> colors)
    {
        foreach (var color in colors)
        {
            if (color.Id == Guid.Empty)
                _dbContext.Set<ColorEntity>().Add(color);
            else
                _dbContext.Set<ColorEntity>().Attach(color);
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

    public void CreateAndInsertProductSizes(Product product, List<SizeEntity> sizes)
    {
        foreach (var size in sizes)
        {
            if (size.Id == Guid.Empty)
                _dbContext.Set<SizeEntity>().Add(size);
            else
                _dbContext.Set<SizeEntity>().Attach(size);
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

}