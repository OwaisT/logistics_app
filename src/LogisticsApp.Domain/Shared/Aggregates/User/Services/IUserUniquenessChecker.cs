namespace LogisticsApp.Domain.Shared.Aggregates.User.Services;

public interface IUserUniquenessChecker
{
    bool IsUnique(string email);
}