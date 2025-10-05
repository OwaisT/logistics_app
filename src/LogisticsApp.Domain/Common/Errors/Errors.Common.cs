using ErrorOr;

namespace LogisticsApp.Domain.Common.Errors;

public static partial class Errors
{
    public static class Common
    {
        public static Error CannotBeEmpty(string fieldName) => Error.Validation(
            code: "Common.CannotBeEmpty",
            description: $"The field '{fieldName}' cannot be empty.");

        public static Error CannotBeNegative(string fieldName) => Error.Validation(
            code: "Common.CannotBeNegative",
            description: $"The field '{fieldName}' cannot be negative.");

        public static Error InvalidInput(string message) => Error.Validation(
            code: "Common.InvalidInput",
            description: message);

        public static Error CannotBeNegativeOrZero(string fieldName) => Error.Validation(
            code: "Common.CannotBeNegativeOrZero",
            description: $"The field '{fieldName}' cannot be negative or zero.");

        public static Error InvalidFormat(string fieldName) => Error.Validation(
            code: "Common.InvalidFormat",
            description: $"The field '{fieldName}' has an invalid format.");

        public static Error EntityNotFound(string entityName, string identifier) => Error.NotFound(
            code: "Common.NotFound",
            description: $"{entityName} with identifier '{identifier}' was not found.");

        public static Error DuplicateEntity(string entityName, List<string> parameters) => Error.Conflict(
            code: "Common.Duplicate",
            description: $"{entityName} with parameters {string.Join(", ", parameters)} already exists.");

        public static Error DuplicatePropertyValue(string entityName, string propertyName, string propertyValue) => Error.Conflict(
            code: "Common.Duplicate",
            description: $"Property {propertyName} with value {propertyValue} already exists for {entityName}.");
    }
}