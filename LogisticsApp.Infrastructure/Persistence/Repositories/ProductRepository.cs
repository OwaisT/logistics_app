using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.Aggregates.Product;
using LogisticsApp.Infrastructure.Persistence.Models;
using LogisticsApp.Domain.Products.ValueObjects;
using LogisticsApp.Domain.Aggregates.Product.ValueObjects;

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
        // Map domain collections to persistence entities
        var sizes = MapSizesToEntities(product.Sizes.ToList());
        var colors = MapColorsToEntities(product.Colors.ToList());
        var categories = MapCategoriesToEntities(product.Categories.ToList());
        var assortmentEntries = MapAssortmentsToEntities(product);

        // Attach or add related entities to context
        foreach (var size in sizes)
        {
            if (size.Id == Guid.Empty)
                _dbContext.Set<Size>().Add(size);
            else
                _dbContext.Set<Size>().Attach(size);
        }
        foreach (var color in colors)
        {
            if (color.Id == Guid.Empty)
                _dbContext.Set<Color>().Add(color);
            else
                _dbContext.Set<Color>().Attach(color);
        }
        foreach (var category in categories)
        {
            if (category.Id == Guid.Empty)
                _dbContext.Set<Category>().Add(category);
            else
                _dbContext.Set<Category>().Attach(category);
        }

        // Add assortment entries to the context
        foreach (var entry in assortmentEntries)
        {
            _dbContext.Set<AssortmentEntry>().Add(entry);
        }

        _dbContext.ProductAssortments.AddRange(assortmentEntries);
        // Add the product aggregate itself
        _dbContext.Products.Add(product);
        _dbContext.SaveChanges();
    }
    // Helper: Map domain sizes (List<string>) to persistence Size entities
    private List<Size> MapSizesToEntities(List<string> sizes)
    {
        return sizes
            .Select(sizeName => _dbContext.Set<Size>().FirstOrDefault(s => s.Name == sizeName) ?? new Size { Name = sizeName })
            .ToList();
    }

    // Helper: Map domain colors (List<string>) to persistence Color entities
    private List<Color> MapColorsToEntities(List<string> colors)
    {
        return colors
            .Select(colorName => _dbContext.Set<Color>().FirstOrDefault(c => c.Name == colorName) ?? new Color { Name = colorName })
            .ToList();
    }

    // Helper: Map domain categories (List<string>) to persistence Category entities
    private List<Category> MapCategoriesToEntities(List<string> categories)
    {
        return categories
            .Select(catName => _dbContext.Set<Category>().FirstOrDefault(c => c.Name == catName) ?? new Category { Name = catName })
            .ToList();
    }

    // Helper: Map domain Assortments to persistence AssortmentEntry entities
    private List<AssortmentEntry> MapAssortmentsToEntities(Product product)
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

    // Helper: Map persistence Size entities to domain List<string>
    private List<string> MapSizesToDomain(List<Size> sizes)
    {
        return sizes.Select(s => s.Name).ToList();
    }

    // Helper: Map persistence Color entities to domain List<string>
    private List<string> MapColorsToDomain(List<Color> colors)
    {
        return colors.Select(c => c.Name).ToList();
    }

    // Helper: Map persistence Category entities to domain List<string>
    private List<string> MapCategoriesToDomain(List<Category> categories)
    {
        return categories.Select(c => c.Name).ToList();
    }

    // Helper: Map persistence AssortmentEntry entities to domain Assortment objects
    private List<Assortment> MapAssortmentsToDomain(List<AssortmentEntry> entries)
    {
        return entries
            .GroupBy(e => e.Color)
            .Select(g => Assortment.Create(g.Key, g.ToDictionary(e => e.Size, e => e.Quantity)))
            .ToList();
    }
}