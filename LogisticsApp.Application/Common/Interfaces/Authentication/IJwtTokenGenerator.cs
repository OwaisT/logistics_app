using LogisticsApp.Domain.Entities;

namespace LogisticsApp.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
