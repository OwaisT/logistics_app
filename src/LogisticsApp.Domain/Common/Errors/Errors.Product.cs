using ErrorOr;

namespace LogisticsApp.Domain.Common.Errors;

public static partial class Errors
{
    public static class Product
    {
        public static Error InvalidProduct(string message) => Error.Validation(
            code: "Product.Invalid",
            description: $"Invalid product: {message}");

        public static Error ColorNotFound(string color) => Error.NotFound(
            code: "Product.ColorNotFound",
            description: $"Color '{color}' not found in product.");

        public static Error DuplicateRefCode(string refCode, string season) => Error.Conflict(
            code: "Product.DuplicateRefCode",
            description: $"A product with RefCode '{refCode}' already exists for the season '{season}'.");

        public static Error DuplicateSeason(string season) => Error.Conflict(
            code: "Product.DuplicateSeason",
            description: $"A product reference with season '{season}' already exists.");

        public static Error SizeNotFound(string size) => Error.NotFound(
            code: "Product.SizeNotFound",
            description: $"Size '{size}' not found in product.");
        
        public static Error ProductInUseCannotBeModified(string productId) => Error.Validation(
            code: "Product.ProductInUseCannotBeModified",
            description: $"Product with ID '{productId}' is currently in use and cannot be modified.");

        public static Error VariationInUse(string color, string size) => Error.Validation(
            code: "Product.VariationInUse",
            description: $"Cannot perform this action because a variation with color '{color}' and size '{size}' is in use. Please make sure that variation is not associated with any active records before proceeding.");
        
        public static Error InsufficientStock(string variationId) => Error.Validation(
            code: "Product.InsufficientStock",
            description: $"Insufficient stock for variation with ID '{variationId}'.");
    }
}
