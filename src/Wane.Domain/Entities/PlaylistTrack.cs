namespace Wane.Domain.Entities;

public class PlaylistTrack
{
    public Guid Id { get; set; }
    public Guid PlaylistId { get; set; }
    public Guid TrackId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public Playlist Playlist { get; set; } = null!;
    public Track Track { get; set; } = null!;
}
