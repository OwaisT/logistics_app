using LogisticsApp.Infrastructure.Persistence.Aggregates.Users.Entities;
using Microsoft.EntityFrameworkCore;

namespace LogisticsApp.Infrastructure.Persistence.Aggregates.Users.Helpers;

public class UserMappingInHelper(
    LogisticsAppDbContext _dbContext)
{
    public List<RoleEntity> MapRolesToEntities(List<string> roleNames)
    {
        return [.. roleNames.Select(roleName => _dbContext.Set<RoleEntity>().FirstOrDefault(r => r.Name == roleName) ?? new RoleEntity { Name = roleName })];
    }

}