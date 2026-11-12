namespace Wane.Domain.Entities;

/// <summary>
/// A named list of tracks that belongs to a <see cref="Profile"/>.
/// </summary>
public class Playlist
{
    /// <summary>Unique identifier of the playlist.</summary>
    public Guid Id { get; set; }

    /// <summary>Identifier of the profile that owns the playlist.</summary>
    public Guid ProfileId { get; set; }

    /// <summary>Display name of the playlist. Required, up to 100 characters.</summary>
    public string? Name { get; set; }

    /// <summary>Moment the playlist was created, in UTC.</summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>Moment the playlist was last changed, in UTC. <c>null</c> if it was never changed.</summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>The profile that owns the playlist.</summary>
    public Profile Profile { get; set; } = null!;

    /// <summary>The entries linking this playlist to its tracks.</summary>
    public List<PlaylistTrack> PlaylistTracks { get; set; } = [];
}
