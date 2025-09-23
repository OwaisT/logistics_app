using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.Shared.Aggregates.User;

namespace LogisticsApp.Infrastructure.Persistence.Users.Repositories;

public class UserRepository : IUserRepository
{
    private static readonly List<User> _users = [];

    public void Add(User user)
    {
        _users.Add(user);
    }

    public User? GetUserByEmail(string email)
    {
        return _users.SingleOrDefault(u => u.Email == email);
    }

}
