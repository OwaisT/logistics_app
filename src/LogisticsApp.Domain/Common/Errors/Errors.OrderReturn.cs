using ErrorOr;

namespace LogisticsApp.Domain.Common.Errors;

public static partial class Errors
{
    public static class OrderReturn
    {
        public static Error InvalidReturnItem(string itemId) => Error.Validation(
            code: "OrderReturn.InvalidReturnItem",
            description: $"The item with ID {itemId} is not valid for return."
        );

        public static Error InvalidReturnItems() => Error.Validation(
            code: "OrderReturn.InvalidReturnItems",
            description: "One or more items are not valid for return."
        );
    }
}
