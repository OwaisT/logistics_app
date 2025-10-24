using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.Shared.Aggregates.User.Services;

namespace LogisticsApp.Application.Authentication.Services;

public class UserUniquenessChecker(IUserRepository _userRepository) : IUserUniquenessChecker
{
    public bool IsUnique(string email)
    {
        return _userRepository.GetUserByEmail(email) is null;
    }
}