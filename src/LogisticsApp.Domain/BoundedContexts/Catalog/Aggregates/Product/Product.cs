using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Product.Entities;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Product.ValueObjects;
using LogisticsApp.Domain.Common.Errors;
using LogisticsApp.Domain.Common.Models;

namespace LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Product;

public sealed class Product : AggregateRoot<ProductId, Guid>
{
    private readonly List<Variation> _variations = [];
    private readonly List<string> _categories = [];
    private readonly List<string> _colors = [];
    private readonly List<string> _sizes = [];
    private readonly List<Assortment> _assortments = [];
    public string RefCode { get; private set; }
    public string Season { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal GeneralPrice { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public bool IsActive { get; private set; }
    public IReadOnlyList<string> Categories => _categories.AsReadOnly();
    public IReadOnlyList<string> Colors => _colors.AsReadOnly();
    public IReadOnlyList<string> Sizes => _sizes.AsReadOnly();
    public IReadOnlyList<Assortment> Assortments => _assortments.AsReadOnly();
    public IReadOnlyList<Variation> Variations => _variations.AsReadOnly();

    internal Product(
        ProductId id,
        string refCode,
        string season,
        string name,
        string description,
        decimal generalPrice,
        DateTime createdAt,
        DateTime updatedAt,
        bool isActive,
        List<string> categories,
        List<string> colors,
        List<string> sizes,
        List<Assortment> assortments,
        List<Variation> variations)
        : base(id)
    {
        RefCode = refCode;
        Season = season;
        Name = name;
        Description = description;
        GeneralPrice = generalPrice;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        IsActive = isActive;
        _categories = categories;
        _colors = colors;
        _sizes = sizes;
        _assortments = assortments;
        _variations = variations;
    }

    public Variation? GetVariation(VariationId variationId)
    {
        return _variations.FirstOrDefault(v => v.Id == variationId);
    }

    public ErrorOr<Product> AddVariation(string color, string size)
    {
        if (_variations.Any(v => v.Color == color && v.Size == size))
        {
            // Variation with the same color and size already exists
            return Errors.Common.DuplicateEntity("Variation", new List<string> { $"Color: {color}", $"Size: {size}" });
        }
        _variations.Add(Variation.Create(
            RefCode,
            Season,
            Name,
            Description,
            GeneralPrice,
            color,
            size));
        UpdatedAt = DateTime.UtcNow;
        return this;
    }

    public ErrorOr<Product> RemoveVariation(VariationId variationId, string color, string size)
    {
        var variation = _variations.FirstOrDefault(v => v.Id == variationId && v.Color == color && v.Size == size);
        if (variation == null)
        {
            // Variation not found
            return Errors.Common.EntityNotFound("Variation", variationId.Value.ToString());
        }
        _variations.Remove(variation);
        UpdatedAt = DateTime.UtcNow;
        return this;
    }

    public ErrorOr<Product> ModifyReceivedForVariation(VariationId variationId, int quantity)
    {
        var variation = _variations.FirstOrDefault(v => v.Id == variationId);
        if (variation == null)
        {
            return Errors.Common.EntityNotFound("Variation", variationId.Value.ToString());
        }
        variation.ModifyReceived(quantity);
        UpdatedAt = DateTime.UtcNow;
        return this;
    }

#pragma warning disable CS8618
    private Product() : base(default!) { }
#pragma warning restore CS8618
}
