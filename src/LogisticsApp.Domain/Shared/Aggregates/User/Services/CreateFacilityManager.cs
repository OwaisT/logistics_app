using ErrorOr;
using LogisticsApp.Domain.Shared.Aggregates.User.Specifications;

namespace LogisticsApp.Domain.Shared.Aggregates.User.Services;

public static class CreateFacilityManager
{
    public static ErrorOr<User> Execute(
        IUserUniquenessChecker userUniquenessChecker,
        string firstName,
        string lastName,
        string email,
        string passwordHash)
    {
        var validateUserSpecificationsResult = UserSpecifications.ValidateUserSpecifications(userUniquenessChecker, email);
        if (validateUserSpecificationsResult.IsError)
        {
            return validateUserSpecificationsResult.Errors;
        }
        var roles = new List<string> { "FacilityManager" };
        return User.Create(
            firstName,
            lastName,
            email,
            passwordHash,
            roles);
    }
}