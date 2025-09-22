using LogisticsApp.Domain.Common.Models;

namespace LogisticsApp.Domain.Products.ValueObjects;

public sealed class Assortment : ValueObject
{
    public string Color { get; private set; }
    public IReadOnlyDictionary<string, int> Sizes { get; private set; }

    private Assortment(string color, IDictionary<string, int> sizes)
    {
        Color = color;
        Sizes = new Dictionary<string, int>(sizes);
    }

    public static Assortment Create(string color, IDictionary<string, int> sizes)
    {
        return new Assortment(color, sizes);
    }

    public int GetQuantity(string size) =>
        Sizes.TryGetValue(size.ToUpper(), out var qty) ? qty : 0;

    public int Total() => Sizes.Values.Sum();

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Color.ToLower();
        foreach (var kv in Sizes.OrderBy(kv => kv.Key))
        {
            yield return kv.Key.ToLower();
            yield return kv.Value;
        }
    }

// #pragma warning disable CS8618
//     private Assortment() { } // For EF Core
// #pragma warning restore CS8618
}

