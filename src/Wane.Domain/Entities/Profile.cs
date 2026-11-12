namespace Wane.Domain.Entities;

/// <summary>
/// A profile of a <see cref="User"/>. It has a name and an optional security code,
/// and it owns the playlists.
/// </summary>
public class Profile
{
    /// <summary>Unique identifier of the profile.</summary>
    public Guid Id { get; set; }

    /// <summary>Identifier of the user that owns the profile.</summary>
    public Guid UserId { get; set; }

    /// <summary>Display name of the profile. Required, unique, up to 100 characters.</summary>
    public string? Name { get; set; }

    /// <summary>Optional security code of the profile, up to 4 characters.</summary>
    public string? Code { get; set; }

    /// <summary>Moment the profile was created, in UTC.</summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>Moment the profile was last changed, in UTC. <c>null</c> if it was never changed.</summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>The playlists owned by the profile.</summary>
    public List<Playlist> Playlists { get; set; } = [];

    /// <summary>The user that owns the profile.</summary>
    public User User { get; set; } = null!;
}
