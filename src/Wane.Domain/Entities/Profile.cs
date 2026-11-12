namespace Wane.Domain.Entities;

/// <summary>
/// Profile entity, it will have a name and a opcional security code;
/// </summary>
public class Profile
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public List<Playlist> Playlists { get; set; } = [];
    public User User { get; set; } = null!;
}
