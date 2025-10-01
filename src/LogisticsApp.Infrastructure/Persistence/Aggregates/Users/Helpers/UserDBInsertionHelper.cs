using LogisticsApp.Domain.Shared.Aggregates.User;
using LogisticsApp.Infrastructure.Persistence.Aggregates.Users.Entities;

namespace LogisticsApp.Infrastructure.Persistence.Aggregates.Users.Helpers;

public class UserDBInsertionHelper(
    LogisticsAppDbContext _dbContext)
{
    public void CreateAndInsertUserRoles(User user, List<RoleEntity> roles)
    {
        foreach (var role in roles)
        {
            if (role.Id == Guid.Empty)
                _dbContext.Set<RoleEntity>().Add(role);
            else
                _dbContext.Set<RoleEntity>().Attach(role);
        }
        var userRoles = roles.Select(role => new Dictionary<string, object>
        {
            ["UserId"] = user.Id,
            ["RoleId"] = role.Id
        }).ToList();
        foreach (var entry in userRoles)
        {
            _dbContext.Set<Dictionary<string, object>>("UserRoles").Add(entry);
        }
    }

}
