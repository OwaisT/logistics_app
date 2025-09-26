using ErrorOr;
using LogisticsApp.Domain.Common.Errors;
using LogisticsApp.Domain.Shared.Aggregates.User.ValueObjects;

namespace LogisticsApp.Domain.Shared.Aggregates.User.Services;

public class UserFactory(IUserUniquenessChecker userUniquenessChecker)
{
    public ErrorOr<User> Create(
        string firstName,
        string lastName,
        string email,
        string passwordHash,
        IEnumerable<string>? roles = null)
    {
        var validateUserSpecificationsResult = ValidateUserSpecifications(email);
        if (validateUserSpecificationsResult.IsError)
        {
            return validateUserSpecificationsResult.Errors;
        }
        return new User(
            UserId.CreateUnique(),
            firstName,
            lastName,
            email,
            passwordHash,
            roles
        );
    }

    private ErrorOr<bool> ValidateUserSpecifications(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return Errors.Common.InvalidInput("Email cannot be empty.");
        }

        if (!IsEmailUnique(email))
        {
            return Errors.Common.DuplicateEntity(nameof(User), [$"Email: {email}"]);
        }
        // Add any additional validation logic here if needed
        return true;
    }
    
    private bool IsEmailUnique(string email)
    {
        return userUniquenessChecker.IsUnique(email);
    }
}