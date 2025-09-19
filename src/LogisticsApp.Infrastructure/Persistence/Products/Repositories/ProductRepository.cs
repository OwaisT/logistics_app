using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.Aggregates.Product;
using LogisticsApp.Domain.Products.ValueObjects;
using LogisticsApp.Infrastructure.Persistence.Products.Helpers;
using LogisticsApp.Infrastructure.Persistence.Products.Models;

namespace LogisticsApp.Infrastructure.Persistence.Products.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly LogisticsAppDbContext _dbContext;
    private static readonly List<Product> _products = [];
    private readonly MappingInHelper _mappingInHelper;
    private readonly DBInsertionHelper _dbInsertionHelper;

    public ProductRepository(LogisticsAppDbContext dbContext)
    {
        _dbContext = dbContext;
        _mappingInHelper = new MappingInHelper(dbContext);
        _dbInsertionHelper = new DBInsertionHelper(dbContext);
    }

    public void Add(Product product)
    {
        // Map domain collections to persistence entities
        // var sizes = _mappingInHelper.MapSizesToEntities(product.Sizes.ToList());
        // _dbInsertionHelper.CreateAndInsertProductSizes(product, sizes);

        // var colors = _mappingInHelper.MapColorsToEntities(product.Colors.ToList());
        // _dbInsertionHelper.CreateAndInsertProductColors(product, colors);

        // var categories = _mappingInHelper.MapCategoriesToEntities(product.Categories.ToList());
        // _dbInsertionHelper.CreateAndInsertProductCategories(product, categories);

        // var assortmentEntries = _mappingInHelper.MapAssortmentsToEntities(product);
        // _dbContext.ProductAssortments.AddRange(assortmentEntries);
        // // Add the product aggregate itself
        // _dbContext.Products.Add(product);
        // _dbContext.SaveChanges();
        _products.Add(product);
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