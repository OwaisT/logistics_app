# Domain Models

## User

```csharp

class User
{
    Guid Id { get; }
    string FirstName { get; }
    string LastName { get; }
    string Email { get; }
    string Password { get; }
    DateTime CreatedAt { get; }
    DateTime UpdatedAt { get; }
    List<string> Roles { get; }
    bool IsActive { get; }

    User Create(
        string firstName, 
        string lastName, 
        string email, 
        string password);
    
    void UpdateProfile(
        string firstName, 
        string lastName, 
        string email);
    
    void ChangePassword(string newPassword);
    
    void AddRole(Guid roleId);

    void RemoveRole(Guid roleId);

    void Activate();
    
    void Deactivate();

}
```

```json
{
    id: "00000",
    firstName: "John",
    lastName: "Doe",
    email: "john.doe@example.com",
    password: "hashed_password",
    createdAt: "2023-01-01T00:00:00Z",
    updatedAt: "2023-01-01T00:00:00Z",
    roles: ["admin", "user"],
    isActive: true
}
```