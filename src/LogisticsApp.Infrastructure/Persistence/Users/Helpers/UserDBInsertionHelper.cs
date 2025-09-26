using LogisticsApp.Domain.Shared.Aggregates.User;
using LogisticsApp.Infrastructure.Persistence.Users.Entities;

namespace LogisticsApp.Infrastructure.Persistence.Users.Helpers;

public class UserDBInsertionHelper
{
    private readonly LogisticsAppDbContext _dbContext;

    public UserDBInsertionHelper(LogisticsAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

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
