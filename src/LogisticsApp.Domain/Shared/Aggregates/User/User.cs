using ErrorOr;
using LogisticsApp.Domain.Common.Errors;
using LogisticsApp.Domain.Common.Models;
using LogisticsApp.Domain.Shared.Aggregates.User.Services;
using LogisticsApp.Domain.Shared.Aggregates.User.ValueObjects;

namespace LogisticsApp.Domain.Shared.Aggregates.User;

public sealed class User : AggregateRoot<UserId, Guid>
{
    private readonly List<string> _roles = [];
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public IReadOnlyList<string> Roles => _roles.AsReadOnly();
    public bool IsActive { get; private set; }


    internal User(
        UserId id,
        string firstName,
        string lastName,
        string email,
        string passwordHash,
        IEnumerable<string>? roles = null)
        : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PasswordHash = passwordHash;
        _roles = roles?.ToList() ?? [];
        CreatedAt = DateTime.UtcNow;
        IsActive = true;
    }

    public void Update(string firstName, string lastName, string email)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ChangePassword(string newPasswordHash)
    {
        PasswordHash = newPasswordHash;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddRole(string role)
    {
        _roles.Add(role);
    }

    public void RemoveRole(string role)
    {
        _roles.Remove(role);
    }

    public void Activate()
    {
        IsActive = true;
    }
    public void Deactivate()
    {
        IsActive = false;
    }

#pragma warning disable CS8618
    private User() : base(default!) { }
#pragma warning restore CS8618
}