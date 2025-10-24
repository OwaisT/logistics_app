using ErrorOr;
using LogisticsApp.Domain.Common.Errors;
using LogisticsApp.Domain.Shared.Aggregates.User.Services;

namespace LogisticsApp.Domain.Shared.Aggregates.User.Specifications;

public static class UserSpecifications
{
    internal static ErrorOr<bool> ValidateUserSpecifications(IUserUniquenessChecker userUniquenessChecker, string email)
    {
        if (!userUniquenessChecker.IsUnique(email))
        {
            return Errors.Common.DuplicateEntity(nameof(User), [$"Email: {email}"]);
        }
        // Add any additional validation logic here if needed
        return true;
    }
}