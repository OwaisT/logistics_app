using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Product;
using LogisticsApp.Infrastructure.Persistence.Products.Models;
using Microsoft.EntityFrameworkCore;

namespace LogisticsApp.Infrastructure.Persistence.Products.Helpers;

public class MappingInHelper
{
    private readonly DbContext _dbContext;

    public MappingInHelper(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Helper: Map domain sizes (List<string>) to persistence Size entities
    public List<Size> MapSizesToEntities(List<string> sizes)
    {
        return sizes
            .Select(sizeName => _dbContext.Set<Size>().FirstOrDefault(s => s.Name == sizeName) ?? new Size { Name = sizeName })
            .ToList();
    }

    // Helper: Map domain colors (List<string>) to persistence Color entities
    public List<Color> MapColorsToEntities(List<string> colors)
    {
        return colors
            .Select(colorName => _dbContext.Set<Color>().FirstOrDefault(c => c.Name == colorName) ?? new Color { Name = colorName })
            .ToList();
    }

    // Helper: Map domain categories (List<string>) to persistence Category entities
    public List<Category> MapCategoriesToEntities(List<string> categories)
    {
        return categories
            .Select(catName => _dbContext.Set<Category>().FirstOrDefault(c => c.Name == catName) ?? new Category { Name = catName })
            .ToList();
    }

    // Helper: Map domain Assortments to persistence AssortmentEntry entities
    public List<AssortmentEntry> MapAssortmentsToEntities(Product product)
    {
        var assortmentEntries = product.Assortments
            .SelectMany(a => a.Sizes.Select(s => new AssortmentEntry
            {
                ProductId = product.Id, // infrastructure only
                Color = a.Color,
                Size = s.Key,
                Quantity = s.Value
            }))
            .ToList();
        return assortmentEntries;
    }
}
