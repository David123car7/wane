namespace Wane.Domain.Entities;

public class TrackFile
{
    public Guid Id { get; set; }
    public Guid TrackId { get; set; }
    public string? StorageKey { get; set; }
    public string? Format { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public Track Track { get; set; } = null!;
}
