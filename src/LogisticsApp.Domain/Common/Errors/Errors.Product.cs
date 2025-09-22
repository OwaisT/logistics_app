using ErrorOr;

namespace LogisticsApp.Domain.Common.Errors;

public static partial class Errors
{
    public static class Product
    {
        public static Error InvalidProduct(string message) => Error.Validation(
            code: "Product.Invalid",
            description: $"Invalid product: {message}");
    }
}