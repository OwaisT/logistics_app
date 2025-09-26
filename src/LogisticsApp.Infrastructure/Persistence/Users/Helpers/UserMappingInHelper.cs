using LogisticsApp.Infrastructure.Persistence.Users.Entities;
using Microsoft.EntityFrameworkCore;

namespace LogisticsApp.Infrastructure.Persistence.Users.Helpers;

public class UserMappingInHelper
{
    private readonly LogisticsAppDbContext _dbContext;

    public UserMappingInHelper(LogisticsAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<RoleEntity> MapRolesToEntities(List<string> roleNames)
    {
        return [.. roleNames.Select(roleName => _dbContext.Set<RoleEntity>().FirstOrDefault(r => r.Name == roleName) ?? new RoleEntity { Name = roleName })];
    }

}