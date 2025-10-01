namespace LogisticsApp.Infrastructure.Persistence.Aggregates.Users.Entities;

public class RoleEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}