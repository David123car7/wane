namespace Wane.Domain.Entities;

/// <summary>
/// User entity to represent the user, the user will have a username and password must be hashed
/// </summary>
public class User
{
    public Guid Id { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
}
