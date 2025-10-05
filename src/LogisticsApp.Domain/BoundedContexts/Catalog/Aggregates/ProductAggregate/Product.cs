using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.Entities;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;
using LogisticsApp.Domain.Common.Errors;
using LogisticsApp.Domain.Common.Models;

namespace LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate;

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

    private Product(
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

    public static Product Create(
        string refCode,
        string season,
        string name,
        string description,
        decimal generalPrice,
        bool isActive,
        List<string> categories,
        List<string> colors,
        List<string> sizes,
        List<Assortment> assortments,
        List<Variation> variations)
    {
        return new Product(
            ProductId.CreateUnique(),
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
    }

    public Variation? GetVariation(VariationId variationId)
    {
        return _variations.FirstOrDefault(v => v.Id == variationId);
    }

    internal Product AddVariations(List<Variation> variations)
    {
        _variations.AddRange(variations);
        return this;
    }

    public void AddCategory(string category)
    {
        if (!_categories.Contains(category))
        {
            _categories.Add(category);
            UpdatedAt = DateTime.UtcNow;
        }
    }

    public void RemoveCategory(string category)
    {
        if (_categories.Contains(category))
        {
            _categories.Remove(category);
            UpdatedAt = DateTime.UtcNow;
        }
    }

    public Product AddColor(string color)
    {
        _colors.Add(color);
        UpdatedAt = DateTime.UtcNow;
        return this;
    }

    public void RemoveColor(string color)
    {
        if (_colors.Contains(color))
        {
            _colors.Remove(color);
            UpdatedAt = DateTime.UtcNow;
        }
    }

    public void AddSize(string size)
    {
        if (!_sizes.Contains(size))
        {
            _sizes.Add(size);
            UpdatedAt = DateTime.UtcNow;
        }
    }

    public void RemoveSize(string size)
    {
        if (_sizes.Contains(size))
        {
            _sizes.Remove(size);
            UpdatedAt = DateTime.UtcNow;
        }
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
