using System.Reflection;
using Mapster;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate;
using LogisticsApp.Infrastructure.Persistence.Aggregates.Products.Entities;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.Entities;

namespace LogisticsApp.Infrastructure.Persistence.Aggregates.Products.Helpers;

public static class ProductRehydrationHelper
{
    public static Product Rehydrate(ProductEntity src)
    {
        if (src == null) return null!;

        var productType = typeof(Product);

        // Create instance using non-public parameterless ctor
        var product = (Product)Activator.CreateInstance(productType, true)!;

        // Set Id property (protected setter) using reflection
        var idProp = productType.GetProperty("Id", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)!;
        idProp.SetValue(product, ProductId.Create(src.Id));

        // Set simple properties with private setters
        void SetProp(string name, object? value)
        {
            var p = productType.GetProperty(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (p != null)
            {
                p.SetValue(product, value);
            }
        }

        SetProp(nameof(Product.RefCode), src.RefCode);
        SetProp(nameof(Product.Season), src.Season);
        SetProp(nameof(Product.Name), src.Name);
        SetProp(nameof(Product.Description), src.Description);
        SetProp(nameof(Product.GeneralPrice), src.GeneralPrice);
        SetProp(nameof(Product.CreatedAt), src.CreatedAt);
        SetProp(nameof(Product.UpdatedAt), src.UpdatedAt);
        SetProp(nameof(Product.IsActive), src.IsActive);

        // Set private backing fields for collections
        FieldInfo? GetField(string name) => productType.GetField(name, BindingFlags.Instance | BindingFlags.NonPublic);

        // Categories, Colors, Sizes are stored as private List<string> fields
        var categoriesField = GetField("_categories");
        var colorsField = GetField("_colors");
        var sizesField = GetField("_sizes");
        var assortmentsField = GetField("_assortments");
        var variationsField = GetField("_variations");

        var categories = src.Categories != null ? src.Categories.Select(c => c.Name).ToList() : new List<string>();
        var colors = src.Colors != null ? src.Colors.Select(c => c.Name).ToList() : new List<string>();
        var sizes = src.Sizes != null ? src.Sizes.Select(s => s.Name).ToList() : new List<string>();
        var assortments = src.Assortments != null ? src.Assortments : new List<Assortment>();

    // Map VariationEntity to Variation (domain entity) using Mapster
        var variations = src.Variations != null ? src.Variations.Adapt<List<Variation>>() : new List<Variation>();

        if (categoriesField != null) categoriesField.SetValue(product, categories);
        if (colorsField != null) colorsField.SetValue(product, colors);
        if (sizesField != null) sizesField.SetValue(product, sizes);
        if (assortmentsField != null) assortmentsField.SetValue(product, assortments);
        if (variationsField != null) variationsField.SetValue(product, variations);

        return product;
    }
}
