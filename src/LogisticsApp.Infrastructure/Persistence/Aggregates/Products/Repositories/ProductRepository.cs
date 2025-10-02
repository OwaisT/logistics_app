using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;
using LogisticsApp.Infrastructure.Persistence.Aggregates.Products.Helpers;
// using LogisticsApp.Infrastructure.Persistence.Products.Helpers;
// using LogisticsApp.Infrastructure.Persistence.Products.Models;

namespace LogisticsApp.Infrastructure.Persistence.Aggregates.Products.Repositories;

public class ProductRepository(
    LogisticsAppDbContext _dbContext,
    ProductMappingInHelper _mappingInHelper,
    ProductDBInsertionHelper _dbInsertionHelper,
    ProductDBExtractionHelper _dbExtractionHelper) : IProductRepository
{
    private static readonly List<Product> _products = [];

    public void Add(Product product)
    {
        var sizes = _mappingInHelper.MapSizesToEntities([.. product.Sizes]);
        _dbInsertionHelper.CreateAndInsertProductSizes(product, sizes);

        var colors = _mappingInHelper.MapColorsToEntities(product.Colors.ToList());
        _dbInsertionHelper.CreateAndInsertProductColors(product, colors);

        var categories = _mappingInHelper.MapCategoriesToEntities(product.Categories.ToList());
        _dbInsertionHelper.CreateAndInsertProductCategories(product, categories);

        // // Add the product aggregate itself
        _dbContext.Add(product);
        _dbContext.SaveChanges();
    }

    public void Update(Product product)
    {
        // var sizes = _mappingInHelper.MapSizesToEntities([.. product.Sizes]);
        // _dbInsertionHelper.CreateAndInsertProductSizes(product, sizes);

        // var colors = _mappingInHelper.MapColorsToEntities(product.Colors.ToList());
        // _dbInsertionHelper.CreateAndInsertProductColors(product, colors);

        // var categories = _mappingInHelper.MapCategoriesToEntities(product.Categories.ToList());
        // _dbInsertionHelper.CreateAndInsertProductCategories(product, categories);

        _dbContext.Update(product);
        _dbContext.SaveChanges();
    }

    public List<Product> GetAll()
    {
        // Fetch products with related entities from the database
        var products = _dbContext.Products.ToList();
        var completeProducts = new List<Product>();
        foreach (var product in products)
        {
            var completeProduct = MapToAggregate(product);
            completeProducts.Add(completeProduct);
        }
        return completeProducts;
    }

    public async Task<List<Product>> GetAllAsync()
    {
        // Fetch products with related entities from the database
        return await Task.FromResult(_products);
    }

    public Product? GetById(ProductId id)
    {
        var product = _dbContext.Products.FirstOrDefault(p => p.Id == id);
        if (product == null) return null;
        return MapToAggregate(product);
    }

    public Product? GetByDetails(string refCode, string season)
    {
        var product = _dbContext.Products.FirstOrDefault(p => p.RefCode == refCode && p.Season == season);
        if (product == null) return null;
        return MapToAggregate(product);
    }

    private Product MapToAggregate(Product product)
    {
        var categoryIds = _dbExtractionHelper.GetProductCategoryIds(product.Id.Value);
        var categoryNames = ProductMappingOutHelper.MapCategoryEntitiesToNames(_dbExtractionHelper.GetCategoriesByIds(categoryIds));
        product = ProductMappingOutHelper.MapCategoriesToProductAggregate(product, categoryNames);
        var sizeIds = _dbExtractionHelper.GetProductSizeIds(product.Id.Value);
        var sizesNames = ProductMappingOutHelper.MapSizeEntitiesToSizes(_dbExtractionHelper.GetSizesByIds(sizeIds));
        product = ProductMappingOutHelper.MapSizesToProductAggregate(product, sizesNames);
        var colorIds = _dbExtractionHelper.GetProductColorIds(product.Id.Value);
        var colorNames = ProductMappingOutHelper.MapColorEntitiesToColors(_dbExtractionHelper.GetColorsByIds(colorIds));
        product = ProductMappingOutHelper.MapColorsToProductAggregate(product, colorNames);

        return product;
    }
}