using Microsoft.EntityFrameworkCore;
using Wane.Domain.Entities;

namespace Wane.Infrastructure.Persistence;

/// <summary>
/// The EF Core context of Wane. The mapping of each entity lives in its own
/// <see cref="IEntityTypeConfiguration{TEntity}"/> and is applied from this assembly.
/// </summary>
public class WaneDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Profile> Profiles { get; set; }
    public DbSet<Track> Tracks { get; set; }
    public DbSet<TrackFile> TrackFiles { get; set; }
    public DbSet<Playlist> Playlists { get; set; }
    public DbSet<PlaylistTrack> PlaylistTracks { get; set; }

    /// <summary>Creates the context with the given options.</summary>
    /// <param name="options">The options that configure the context, such as the database provider.</param>
    public WaneDbContext(DbContextOptions<WaneDbContext> options)
        : base(options) { }

    /// <summary>Applies every entity configuration found in this assembly.</summary>
    /// <param name="builder">The builder used to define the model.</param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(WaneDbContext).Assembly);
    }
}
