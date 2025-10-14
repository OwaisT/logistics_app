using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;
using LogisticsApp.Infrastructure.Persistence.Aggregates.Products.Entities;
using LogisticsApp.Infrastructure.Persistence.Aggregates.Products.Helpers;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace LogisticsApp.Infrastructure.Persistence.Aggregates.Products.Repositories;

public class ProductRepository(
    LogisticsAppDbContext _dbContext) : IProductRepository
{
    // private static readonly List<Product> _products = [];

    public void Add(Product domainProduct)
    {
        var productEntity = domainProduct.Adapt<ProductEntity>();

        productEntity.Categories = EntityResolvingHelpers.ResolveCategories(domainProduct, _dbContext);
        productEntity.Sizes = EntityResolvingHelpers.ResolveSizes(domainProduct, _dbContext);
        productEntity.Colors = EntityResolvingHelpers.ResolveColors(domainProduct, _dbContext);

        _dbContext.Add(productEntity);
        _dbContext.SaveChanges();
    }

    public void Update(Product domainProduct)
    {
        var existing = _dbContext.Products
            .Include(p => p.Variations)
            .Include(p => p.Categories)
            .Include(p => p.Colors)
            .Include(p => p.Sizes)
            .FirstOrDefault(p => p.Id == domainProduct.Id.Value);
        
        if (existing == null)
        {
            // TODO: Use error or
            throw new InvalidOperationException($"Product with ID {domainProduct.Id} not found.");
        }


        domainProduct.Adapt(existing);

        existing.Categories = EntityResolvingHelpers.ResolveCategories(domainProduct, _dbContext);
        existing.Sizes = EntityResolvingHelpers.ResolveSizes(domainProduct, _dbContext);
        existing.Colors = EntityResolvingHelpers.ResolveColors(domainProduct, _dbContext);

        _dbContext.SaveChanges();
    }

    public List<Product> GetAll()
    {
        // Fetch products with related entities from the database
        var productsEntities = _dbContext.Products
            .Include(p => p.Variations)
            .Include(p => p.Categories)
            .Include(p => p.Colors)
            .Include(p => p.Sizes)
            .ToList();

        var products = productsEntities.Adapt<List<Product>>();

        return products;
    }

    public async Task<List<Product>> GetAllAsync()
    {
        var productsEntities = await _dbContext.Products
            .Include(p => p.Variations)
            .Include(p => p.Categories)
            .Include(p => p.Colors)
            .Include(p => p.Sizes)
            .ToListAsync();

        return productsEntities.Adapt<List<Product>>();
    }

    public Product? GetById(ProductId id)
    {
        var productEntity = _dbContext.Products
            .Include(p => p.Variations)
            .Include(p => p.Categories)
            .Include(p => p.Colors)
            .Include(p => p.Sizes)
            .FirstOrDefault(p => p.Id == id.Value);

        if (productEntity == null) return null;
        return productEntity.Adapt<Product>();
    }

    public Product? GetByDetails(string refCode, string season)
    {
        var productEntity = _dbContext.Products
            .Include(p => p.Variations)
            .Include(p => p.Categories)
            .Include(p => p.Colors)
            .Include(p => p.Sizes)
            .FirstOrDefault(p => p.RefCode == refCode && p.Season == season);

        if (productEntity == null) return null;
        return productEntity.Adapt<Product>();
    }

}