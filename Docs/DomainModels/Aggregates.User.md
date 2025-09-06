# Domain Models

## User

```csharp

class User
{
    User Create(
        string id, 
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

    void Deactivate();
    
    void Activate();
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