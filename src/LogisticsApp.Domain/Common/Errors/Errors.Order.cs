using ErrorOr;

namespace LogisticsApp.Domain.Common.Errors;

public static partial class Errors
{
    public static class Order
    {
        public static Error OutOfStock(string variationRefCode) => Error.Conflict(
            code: "Order.OutOfStock",
            description: $"The requested product variation {variationRefCode} is out of stock."
        );
    }
}
