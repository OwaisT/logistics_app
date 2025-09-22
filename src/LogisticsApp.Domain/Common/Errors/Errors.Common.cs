using ErrorOr;

namespace LogisticsApp.Domain.Common.Errors;

public static partial class Errors
{
    public static class Common
    {
        public static Error CannotBeEmpty(string fieldName) => Error.Failure(
            code: "Common.CannotBeEmpty",
            description: $"The field '{fieldName}' cannot be empty.");

        public static Error CannotBeNegative(string fieldName) => Error.Failure(
            code: "Common.CannotBeNegative",
            description: $"The field '{fieldName}' cannot be negative.");

        public static Error InvalidFormat(string fieldName) => Error.Failure(
            code: "Common.InvalidFormat",
            description: $"The field '{fieldName}' has an invalid format.");
    }
}