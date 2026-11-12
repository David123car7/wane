namespace Wane.Domain.Entities;

/// <summary>
/// A song of the catalog. Its audio lives in one or more <see cref="TrackFile"/> entries.
/// </summary>
public class Track
{
    /// <summary>Unique identifier of the track.</summary>
    public Guid Id { get; set; }

    /// <summary>Title of the track. Required, unique, up to 100 characters.</summary>
    public string? Title { get; set; }

    /// <summary>Artist of the track. Optional, up to 100 characters.</summary>
    public string? Artist { get; set; }

    /// <summary>Genre of the track. Required, up to 100 characters.</summary>
    public string? Genre { get; set; }

    /// <summary>Release year of the track.</summary>
    public short Year { get; set; } // Int16

    /// <summary>Moment the track was created, in UTC.</summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>Moment the track was last changed, in UTC. <c>null</c> if it was never changed.</summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>The audio files of the track.</summary>
    public List<TrackFile> TrackFiles { get; set; } = [];

    /// <summary>The entries that place the track in playlists.</summary>
    public List<PlaylistTrack> PlaylistTracks { get; set; } = [];
}
