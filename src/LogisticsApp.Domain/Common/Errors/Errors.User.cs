using ErrorOr;

namespace LogisticsApp.Domain.Common.Errors;

public static partial class Errors
{
    public static class User
    {
        public static Error DuplicateEmail => Error.Conflict(
            code: "User.DuplicateEmail",
            description: "Email is already in use.");
        
        public static Error InvalidRoles => Error.Validation(
            code: "User.InvalidRoles",
            description: "User roles are invalid.");
    }
}