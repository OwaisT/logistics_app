using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Product.Entities;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Product.Events;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Product.Services;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Product.ValueObjects;
using LogisticsApp.Domain.Common.Errors;

namespace LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Product;

public class ProductFactory(IProductUniquenessChecker productUniquenessChecker)
{

    public ErrorOr<Product> Create(
        string refCode,
        string season,
        string name,
        string description,
        decimal generalPrice,
        bool isActive,
        List<string> categories,
        List<string> colors,
        List<string> sizes,
        List<Assortment> assortments)
    {
        var validateProductSpecificationsResult = ValidateProductSpecifications(refCode, season);
        if (validateProductSpecificationsResult.IsError)
        {
            return validateProductSpecificationsResult.Errors;
        }
        var variations = GenerateVariations(
            refCode,
            season,
            name,
            description,
            generalPrice,
            colors,
            sizes);

        var productId = ProductId.CreateUnique();

        var product = new Product(
            productId,
            refCode,
            season,
            name,
            description,
            generalPrice,
            DateTime.UtcNow,
            DateTime.UtcNow,
            isActive,
            categories,
            colors,
            sizes,
            assortments,
            variations);

        product.AddDomainEvent(new ProductCreated(product));
        
        return product;

    }

    private ErrorOr<bool> ValidateProductSpecifications(
        string refCode,
        string season)
    {
        if (!UniqueProductPerSeasonSpecification(refCode, season))
        {
            return Errors.Product.InvalidProduct($"A product with RefCode '{refCode}' already exists for the season '{season}'.");
        }

        return true;
    }

    private bool UniqueProductPerSeasonSpecification(string refCode, string season)
    {
        return productUniquenessChecker.IsUnique(refCode, season);
    }

    private List<Variation> GenerateVariations(
        string refCode,
        string season,
        string name,
        string description,
        decimal generalPrice,
        List<string> colors,
        List<string> sizes)
    {
        var variations = new List<Variation>();

        foreach (var color in colors)
        {
            foreach (var size in sizes)
            {
                var variationName = $"{name} - {color} - {size}";
                var variation = Variation.Create(
                    refCode,
                    season,
                    variationName,
                    description,
                    generalPrice,
                    color,
                    size);
                variations.Add(variation);
            }
        }

        return variations;
    }
}
