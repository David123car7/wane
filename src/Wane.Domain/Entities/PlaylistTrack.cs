namespace Wane.Domain.Entities;

/// <summary>
/// Join entity that places a <see cref="Track"/> in a <see cref="Playlist"/>.
/// </summary>
public class PlaylistTrack
{
    /// <summary>Unique identifier of the entry.</summary>
    public Guid Id { get; set; }

    /// <summary>Identifier of the playlist the track belongs to.</summary>
    public Guid PlaylistId { get; set; }

    /// <summary>Identifier of the track placed in the playlist.</summary>
    public Guid TrackId { get; set; }

    /// <summary>Moment the track was added to the playlist, in UTC.</summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>Moment the entry was last changed, in UTC. <c>null</c> if it was never changed.</summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>The playlist the track belongs to.</summary>
    public Playlist Playlist { get; set; } = null!;

    /// <summary>The track placed in the playlist.</summary>
    public Track Track { get; set; } = null!;
}
