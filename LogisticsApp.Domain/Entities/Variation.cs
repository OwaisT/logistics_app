using LogisticsApp.Domain.ValueObjects;

namespace LogisticsApp.Domain.Entities;

public class Variation
{
    public string Color { get; private set; }
    public string Size { get; private set; }

    public VariationId Id { get; private set; }
    public decimal Price { get; private set; }

    public Variation(string color, string size, string productRef, string season)
    {
        Id = new VariationId(productRef, season, color, size);
        Color = color;
        Size = size;
    }

}
