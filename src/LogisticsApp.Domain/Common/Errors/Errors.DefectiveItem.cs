using ErrorOr;

namespace LogisticsApp.Domain.Common.Errors;

public static partial class Errors
{
    public static class DefectiveItem
    {
        public static Error NotRepairable() => Error.Validation(
            code: "Defective.NotRepairable",
            description: "The defective item cannot be repaired.");
    }
}