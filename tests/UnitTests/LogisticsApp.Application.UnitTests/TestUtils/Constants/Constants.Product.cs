using LogisticsApp.Application.Products.Commands.CreateProduct;
using LogisticsApp.Domain.Products.ValueObjects;

namespace LogisticsApp.Application.UnitTests.TestUtils.Constants;

public static partial class Constants
{
    public static class Product
    {
        public const string RefCode = "Product Ref";
        public const string Season = "Product Season";
        public const string Name = "Product Name";
        public const string Description = "Product Description";
        public const bool IsActive = true;
        public static string Category = "Category";
        public static List<string> CategoriesFromCount(int count) =>
            Enumerable.Range(1, count).Select(i => $"{Category} {i}").ToList();
        public static string Color = "Color";
        public static List<string> ColorsFromCount(int count) =>
            Enumerable.Range(1, count).Select(i => $"{Color} {i}").ToList();
        public static string Size = "Size";
        public static List<string> SizesFromCount(int count) =>
            Enumerable.Range(1, count).Select(i => $"{Size} {i}").ToList();
        public static readonly List<string> Colors = new() { "Red", "Blue" };
        public static readonly List<string> Sizes = new() { "S", "M", "L", "XL", "XXL" };

        public static string AssortmentColor = "Assortment Color";
        public static Dictionary<string, int> AssortmentSizes = new() { { "S", 1 }, { "M", 2 }, { "L", 2 }, { "XL", 2 }, { "XXL", 1 } };

        public static string AssortmentColorFromIndex(int index)
            => $"{AssortmentColor} {index}";


    }
}