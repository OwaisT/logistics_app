using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate;
using LogisticsApp.Infrastructure.Persistence.Aggregates.Products.Entities;
using Microsoft.EntityFrameworkCore;

namespace LogisticsApp.Infrastructure.Persistence.Aggregates.Products.Helpers;

internal static class EntityResolvingHelpers
{
    internal static List<CategoryEntity> ResolveCategories(Product product, LogisticsAppDbContext dbContext)
    {
        var existingCategories = dbContext.Categories
        .Where(c => product.Categories.Select(x => x).Contains(c.Name))
        .ToList();

        var newCategoryNames = product.Categories
            .Select(c => c)
            .Except(existingCategories.Select(c => c.Name))
            .ToList();

        var newCategories = newCategoryNames
            .Select(name => new CategoryEntity { Name = name })
            .ToList();

        dbContext.Categories.AddRange(newCategories);

        return existingCategories.Concat(newCategories).ToList();
    }

    internal static List<SizeEntity> ResolveSizes(Product product, LogisticsAppDbContext dbContext)
    {
        var existingSizes = dbContext.Sizes
        .Where(s => product.Sizes.Select(x => x).Contains(s.Name))
        .ToList();

        var newSizeNames = product.Sizes
            .Select(s => s)
            .Except(existingSizes.Select(s => s.Name))
            .ToList();

        var newSizes = newSizeNames
            .Select(name => new SizeEntity { Name = name })
            .ToList();

        dbContext.Sizes.AddRange(newSizes);

        return existingSizes.Concat(newSizes).ToList();
    }

    internal static List<ColorEntity> ResolveColors(Product product, LogisticsAppDbContext dbContext)
    {
        var existingColors = dbContext.Colors
        .Where(c => product.Colors.Select(x => x).Contains(c.Name))
        .ToList();

        var newColorNames = product.Colors
            .Select(c => c)
            .Except(existingColors.Select(c => c.Name))
            .ToList();

        var newColors = newColorNames
            .Select(name => new ColorEntity { Name = name })
            .ToList();

        dbContext.Colors.AddRange(newColors);

        return existingColors.Concat(newColors).ToList();
    }

    
}