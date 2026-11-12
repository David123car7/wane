namespace Wane.Domain.Entities;

public class Track
{
    public Guid Id { get; set; }
    public string? Tittle { get; set; }
    public string? Artist { get; set; }
    public string? Genre { get; set; }
    public short Year { get; set; } // Int16
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public List<TrackFile> TrackFiles { get; set; } = [];
    public List<PlaylistTrack> PlaylistTracks { get; set; } = [];
}
