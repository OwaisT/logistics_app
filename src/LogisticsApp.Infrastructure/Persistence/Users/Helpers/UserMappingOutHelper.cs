using LogisticsApp.Domain.Shared.Aggregates.User;
using LogisticsApp.Infrastructure.Persistence.Users.Entities;

namespace LogisticsApp.Infrastructure.Persistence.Users.Helpers;

public class UserMappingOutHelper
{
    // Implement mapping from persistence entities to domain models if needed
    public List<string> MapRoleEntitiesToNames(List<RoleEntity> roleEntities)
    {
        return roleEntities.Select(re => re.Name).ToList();
    }

    public User MapRolesToUserAggregate(User user, List<string> roleNames)
    {
        foreach (var roleName in roleNames)
        {
            user.AddRole(roleName);
        }
        return user;
    }
}