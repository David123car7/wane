namespace Wane.Domain.Entities;

/// <summary>
/// An audio file of a <see cref="Track"/>, stored outside the database and found through its storage key.
/// </summary>
public class TrackFile
{
    /// <summary>Unique identifier of the file.</summary>
    public Guid Id { get; set; }

    /// <summary>Identifier of the track the file belongs to.</summary>
    public Guid TrackId { get; set; }

    /// <summary>Key that locates the file in storage. Required, unique, up to 200 characters.</summary>
    public string? StorageKey { get; set; }

    /// <summary>Audio format of the file. Required, up to 5 characters.</summary>
    public string? Format { get; set; }

    /// <summary>Moment the file was registered, in UTC.</summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>Moment the file was last changed, in UTC. <c>null</c> if it was never changed.</summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>The track the file belongs to.</summary>
    public Track Track { get; set; } = null!;
}
