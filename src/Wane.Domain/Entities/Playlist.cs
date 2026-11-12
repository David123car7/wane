using Microsoft.VisualBasic;

namespace Wane.Domain.Entities;

public class Playlist
{
    public Guid Id { get; set; }
    public Guid ProfileId { get; set; }
    public string? Name { get; set; }
    public DateTime CreatedAt { get; set; } = DateAndTime.Now;
    public DateTime? UpdatedAt { get; set; }

    public Profile Profile { get; set; } = null!;
    public List<PlaylistTrack> PlaylistTracks { get; set; } = [];
}
