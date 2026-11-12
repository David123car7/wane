namespace Wane.Domain.Entities;

/// <summary>
/// An account of the platform. It has a username and a password, which must be stored hashed,
/// and it owns one or more profiles.
/// </summary>
public class User
{
    /// <summary>Unique identifier of the user.</summary>
    public Guid Id { get; set; }

    /// <summary>Name used to log in. Required, unique, up to 100 characters.</summary>
    public string? Username { get; set; }

    /// <summary>Hashed password of the user. Required, up to 256 characters. Never the plain text.</summary>
    public string? Password { get; set; }

    /// <summary>Moment the user was created, in UTC.</summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>Moment the user was last changed, in UTC. <c>null</c> if it was never changed.</summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>The profiles owned by the user.</summary>
    public List<Profile> Profiles { get; set; } = [];
}
