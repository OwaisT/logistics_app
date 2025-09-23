using LogisticsApp.Domain.Shared.Aggregates.User;

namespace LogisticsApp.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
