using LogisticsApp.Infrastructure.Persistence.Aggregates.Users.Entities;

namespace LogisticsApp.Infrastructure.Persistence.Aggregates.Users.Helpers;

public class UserDBExtractionHelper(LogisticsAppDbContext _dbContext)
{
    // Implement mapping from persistence entities to domain models if needed

    public List<Guid> GetUserRoleIds(Guid userId)
    {
        // Assuming UserRoles is a junction table with columns UserId and RoleId
        // Use raw SQL or EF Core's DbContext to query the junction table
        var userRoleEntries = _dbContext.Set<Dictionary<string, object>>("UserRoles")
            .Where(e => (Guid)e["UserId"] == userId)
            .ToList();

        var roleIds = userRoleEntries
            .Select(e => (Guid)e["RoleId"])
            .ToList();

        return roleIds;
    }

    public List<RoleEntity> GetRolesByIds(List<Guid> roleIds)
    {
        return _dbContext.Set<RoleEntity>()
            .Where(r => roleIds.Contains(r.Id))
            .ToList();
    }

}