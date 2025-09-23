using ErrorOr;

namespace LogisticsApp.Domain.Common.Errors;

public static partial class Errors
{
    public static class Carton
    {
        public static Error LocationNotUnique => Error.Conflict(
            code: "Carton.LocationNotUnique",
            description: "The specified location for the carton is already occupied by another carton."
        );
    }
}