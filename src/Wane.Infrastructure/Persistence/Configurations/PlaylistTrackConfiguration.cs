using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wane.Domain.Entities;

namespace Wane.Infrastructure.Persistence.Configurations;

/// <summary>
/// Maps <see cref="PlaylistTrack"/> to the <c>playlistTracks</c> table.
/// </summary>
public class PlaylistTrackConfiguration : IEntityTypeConfiguration<PlaylistTrack>
{
    /// <summary>
    /// Sets the table, the key and the two required relations, to the playlist and to the track.
    /// Neither can be deleted while an entry references it.
    /// </summary>
    /// <param name="builder">The builder of the entity.</param>
    public void Configure(EntityTypeBuilder<PlaylistTrack> builder)
    {
        builder.ToTable("playlistTracks");
        builder.HasKey(p => p.Id);
        builder
            .HasOne(p => p.Playlist)
            .WithMany(p => p.PlaylistTracks)
            .HasForeignKey(p => p.PlaylistId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(p => p.Track)
            .WithMany(p => p.PlaylistTracks)
            .HasForeignKey(p => p.TrackId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
        builder.Property(p => p.CreatedAt).IsRequired();
        builder.Property(p => p.UpdatedAt);
    }
}
