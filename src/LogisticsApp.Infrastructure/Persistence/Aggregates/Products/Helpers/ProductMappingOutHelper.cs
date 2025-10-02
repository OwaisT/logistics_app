using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate;
using LogisticsApp.Infrastructure.Persistence.Aggregates.Products.Entities;

namespace LogisticsApp.Infrastructure.Persistence.Aggregates.Products.Helpers;

public class ProductMappingOutHelper
{
    // Implement mapping from persistence entities to domain models if needed
    public static List<string> MapCategoryEntitiesToNames(List<CategoryEntity> categoryEntities)
    {
        return categoryEntities.Select(ce => ce.Name).ToList();
    }

    public static Product MapCategoriesToProductAggregate(Product product, List<string> categoryNames)
    {
        foreach (var categoryName in categoryNames)
        {
            product.AddCategory(categoryName);
        }
        return product;
    }

    public static List<string> MapSizeEntitiesToSizes(List<SizeEntity> sizeEntities)
    {
        return sizeEntities.Select(se => se.Name).ToList();
    }

    public static Product MapSizesToProductAggregate(Product product, List<string> sizes)
    {
        foreach (var size in sizes)
        {
            product.AddSize(size);
        }
        return product;
    }

    public static List<string> MapColorEntitiesToColors(List<ColorEntity> colorEntities)
    {
        return colorEntities.Select(ce => ce.Name).ToList();
    }

    public static Product MapColorsToProductAggregate(Product product, List<string> colors)
    {
        foreach (var color in colors)
        {
            product.AddColor(color);
        }
        return product;
    }

}