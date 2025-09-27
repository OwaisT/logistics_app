using LogisticsApp.Infrastructure.Persistence.Products.Entities;

namespace LogisticsApp.Infrastructure.Persistence.Products.Helpers;

public class ProductMappingInHelper(
    LogisticsAppDbContext _dbContext)
{
    // Mapping methods for incoming product data
    public List<CategoryEntity> MapCategoriesToEntities(List<string> categoryNames)
    {
        return [.. categoryNames.Select(categoryName => _dbContext.Set<CategoryEntity>().FirstOrDefault(c => c.Name == categoryName) ?? new CategoryEntity { Name = categoryName })];
    }

    public List<ColorEntity> MapColorsToEntities(List<string> colorNames)
    {
        return [.. colorNames.Select(colorName => _dbContext.Set<ColorEntity>().FirstOrDefault(c => c.Name == colorName) ?? new ColorEntity { Name = colorName })];
    }

    public List<SizeEntity> MapSizesToEntities(List<string> sizeNames)
    {
        return [.. sizeNames.Select(sizeName => _dbContext.Set<SizeEntity>().FirstOrDefault(s => s.Name == sizeName) ?? new SizeEntity { Name = sizeName })];
    }

}
