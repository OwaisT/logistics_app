using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.Shared.Aggregates.User;
using LogisticsApp.Infrastructure.Persistence.Users.Helpers;

namespace LogisticsApp.Infrastructure.Persistence.Users.Repositories;

public class UserRepository : IUserRepository
{
    private static readonly List<User> _users = [];
    private readonly LogisticsAppDbContext _dbContext;
    private readonly UserMappingInHelper _mappingInHelper;
    private readonly UserDBInsertionHelper _dbInsertionHelper;

    public UserRepository(LogisticsAppDbContext dbContext, UserMappingInHelper mappingInHelper, UserDBInsertionHelper dbInsertionHelper)
    {
        _dbContext = dbContext;
        _mappingInHelper = mappingInHelper;
        _dbInsertionHelper = dbInsertionHelper;
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
        return _dbContext.Users.FirstOrDefault(u => u.Email == email);
    }

}
