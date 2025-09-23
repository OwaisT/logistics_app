using LogisticsApp.Domain.Shared.Aggregates.User;

namespace LogisticsApp.Application.Common.Interfaces.Persistence;
public interface IUserRepository
{
    User? GetUserByEmail(string email);
    void Add(User user);

}
