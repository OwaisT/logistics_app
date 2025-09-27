using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.Shared.Aggregates.User;
using LogisticsApp.Infrastructure.Persistence.Users.Helpers;
using Microsoft.EntityFrameworkCore;

namespace LogisticsApp.Infrastructure.Persistence.Users.Repositories;

public class UserRepository : IUserRepository
{
    private static readonly List<User> _users = [];
    private readonly LogisticsAppDbContext _dbContext;
    private readonly UserMappingInHelper _mappingInHelper;
    private readonly UserDBInsertionHelper _dbInsertionHelper;
    private readonly UserMappingOutHelper _mappingOutHelper;
    private readonly UserDBExtractionHelper _dbExtractionHelper;

    public UserRepository(
        LogisticsAppDbContext dbContext,
        UserMappingInHelper mappingInHelper,
        UserDBInsertionHelper dbInsertionHelper,
        UserMappingOutHelper mappingOutHelper,
        UserDBExtractionHelper dbExtractionHelper)
    {
        _dbContext = dbContext;
        _mappingInHelper = mappingInHelper;
        _mappingOutHelper = mappingOutHelper;
        _dbInsertionHelper = dbInsertionHelper;
        _dbExtractionHelper = dbExtractionHelper;
    }

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
