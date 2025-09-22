using LogisticsApp.Domain.Aggregates.Product.Entities;
using LogisticsApp.Domain.Aggregates.Product.Events;
using LogisticsApp.Domain.Aggregates.Product.Exceptions;
using LogisticsApp.Domain.Aggregates.Product.Services;
using LogisticsApp.Domain.Aggregates.Product.ValueObjects;
using LogisticsApp.Domain.Common.Exceptions;
using LogisticsApp.Domain.Products.ValueObjects;

namespace LogisticsApp.Domain.Aggregates.Product;

public class ProductFactory(IProductUniquenessChecker productUniquenessChecker)
{

    public Product Create(
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
        validateProductSpecifications(refCode, season, generalPrice);

        var variations = generateVariations(
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

    private void validateProductSpecifications(
        string refCode,
        string season,
        decimal generalPrice)
    {
        if (!uniqueProductPerSeasonSpecification(refCode, season))
        {
            throw new InvalidProductException($"A product with RefCode '{refCode}' already exists for the season '{season}'.");
        }

        if (!priceNotNegativeSpecification(generalPrice))
        {
            throw new CannotBeNegativeException(nameof(generalPrice));
        }
    }

    private bool uniqueProductPerSeasonSpecification(string refCode, string season)
    {
        return productUniquenessChecker.IsUnique(refCode, season);
    }

    private bool priceNotNegativeSpecification(decimal price)
    {
        return price >= 0;
    }

    private List<Variation> generateVariations(
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
