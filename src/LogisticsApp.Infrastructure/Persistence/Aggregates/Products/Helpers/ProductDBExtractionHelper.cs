using LogisticsApp.Infrastructure.Persistence.Aggregates.Products.Entities;

namespace LogisticsApp.Infrastructure.Persistence.Aggregates.Products.Helpers;

public class ProductDBExtractionHelper(LogisticsAppDbContext _dbContext)
{
    // Implement mapping from persistence entities to domain models if needed
    public List<Guid> GetProductCategoryIds(Guid productId)
    {
        // Assuming ProductCategories is a junction table with columns ProductId and CategoryId
        // Use raw SQL or EF Core's DbContext to query the junction table
        var productCategoryEntries = _dbContext.Set<Dictionary<string, object>>("ProductCategories")
            .Where(e => (Guid)e["ProductId"] == productId)
            .ToList();

        var categoryIds = productCategoryEntries
            .Select(e => (Guid)e["CategoryId"])
            .ToList();

        return categoryIds;
    }

    public List<CategoryEntity> GetCategoriesByIds(List<Guid> categoryIds)
    {
        return _dbContext.Set<CategoryEntity>()
            .Where(c => categoryIds.Contains(c.Id))
            .ToList();
    }

    public List<Guid> GetProductColorIds(Guid productId)
    {
        // Assuming ProductColors is a junction table with columns ProductId and ColorId
        // Use raw SQL or EF Core's DbContext to query the junction table
        var productColorEntries = _dbContext.Set<Dictionary<string, object>>("ProductColors")
            .Where(e => (Guid)e["ProductId"] == productId)
            .ToList();

        var colorIds = productColorEntries
            .Select(e => (Guid)e["ColorId"])
            .ToList();

        return colorIds;
    }

    public List<ColorEntity> GetColorsByIds(List<Guid> colorIds)
    {
        return _dbContext.Set<ColorEntity>()
            .Where(c => colorIds.Contains(c.Id))
            .ToList();
    }

    public List<Guid> GetProductSizeIds(Guid productId)
    {
        // Assuming ProductSizes is a junction table with columns ProductId and SizeId
        // Use raw SQL or EF Core's DbContext to query the junction table
        var productSizeEntries = _dbContext.Set<Dictionary<string, object>>("ProductSizes")
            .Where(e => (Guid)e["ProductId"] == productId)
            .ToList();

        var sizeIds = productSizeEntries
            .Select(e => (Guid)e["SizeId"])
            .ToList();

        return sizeIds;
    }

    public List<SizeEntity> GetSizesByIds(List<Guid> sizeIds)
    {
        return _dbContext.Set<SizeEntity>()
            .Where(s => sizeIds.Contains(s.Id))
            .ToList();
    }

}