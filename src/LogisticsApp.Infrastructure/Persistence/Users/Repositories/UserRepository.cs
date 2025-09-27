using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.Shared.Aggregates.User;
using LogisticsApp.Infrastructure.Persistence.Users.Helpers;
using Microsoft.EntityFrameworkCore;

namespace LogisticsApp.Infrastructure.Persistence.Users.Repositories;

public class UserRepository(
    LogisticsAppDbContext _dbContext,
    UserMappingInHelper _mappingInHelper,
    UserDBInsertionHelper _dbInsertionHelper,
    UserMappingOutHelper _mappingOutHelper,
    UserDBExtractionHelper _dbExtractionHelper) : IUserRepository
{
    private static readonly List<User> _users = [];

    public void Add(User user)
    {
        // Map domain collections to persistence entities
        var roles = _mappingInHelper.MapRolesToEntities(user.Roles.ToList());
        _dbInsertionHelper.CreateAndInsertUserRoles(user, roles);

        // Add the user aggregate itself
        _dbContext.Add(user);
        _dbContext.SaveChanges();

    }

    public User? GetUserByEmail(string email)
    {
        var user = _dbContext.Users.FirstOrDefault(u => u.Email == email);
        if (user == null) return null;

        var userRoleIds = _dbExtractionHelper.GetUserRoleIds(user.Id.Value);
        var roleNames = _mappingOutHelper.MapRoleEntitiesToNames(_dbExtractionHelper.GetRolesByIds(userRoleIds));
        user = _mappingOutHelper.MapRolesToUserAggregate(user, roleNames);

        return user;
    }

}
