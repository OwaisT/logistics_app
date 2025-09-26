using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.Shared.Aggregates.User.Services;

namespace LogisticsApp.Application.Authentication.Services;

public class UserUniquenessChecker : IUserUniquenessChecker
{
    private readonly IUserRepository _userRepository;

    public UserUniquenessChecker(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public bool IsUnique(string email)
    {
        return _userRepository.GetUserByEmail(email) is null;
    }
}