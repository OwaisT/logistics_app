using System.Reflection;
using Mapster;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.Entities;
using LogisticsApp.Infrastructure.Persistence.Aggregates.Products.Entities;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;

namespace LogisticsApp.Infrastructure.Persistence.Aggregates.Products.Helpers;

public static class VariationRehydrationHelper
{
    public static Variation Rehydrate(VariationEntity src)
    {
        if (src == null) return null!;

        var type = typeof(Variation);
        var instance = (Variation)Activator.CreateInstance(type, true)!;

        // Set Id
        var idProp = type.GetProperty("Id", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)!;
        idProp.SetValue(instance, VariationId.Create(src.Id));

        // Simple properties
        void SetProp(string name, object? value)
        {
            var p = type.GetProperty(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (p != null)
            {
                p.SetValue(instance, value);
            }
        }

        SetProp(nameof(Variation.ProductRefCode), src.ProductRefCode);
        SetProp(nameof(Variation.ProductSeason), src.ProductSeason);
        SetProp(nameof(Variation.Name), src.Name);
        SetProp(nameof(Variation.Description), src.Description);
        SetProp(nameof(Variation.Price), src.Price);
        SetProp(nameof(Variation.Color), src.Color);
        SetProp(nameof(Variation.Size), src.Size);
        SetProp(nameof(Variation.Received), src.Received);
        SetProp(nameof(Variation.Sold), src.Sold);
        SetProp(nameof(Variation.Returned), src.Returned);
        SetProp(nameof(Variation.Defective), src.Defective);
        SetProp(nameof(Variation.Repaired), src.Repaired);
        SetProp(nameof(Variation.CreatedAt), src.CreatedAt);
        SetProp(nameof(Variation.UpdatedAt), src.UpdatedAt);

        return instance;
    }
}
