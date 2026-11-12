using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wane.Domain.Entities;

namespace Wane.Infrastructure.Persistence.Configurations;

public class PlaylistTrackConfiguration : IEntityTypeConfiguration<PlaylistTrack>
{
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
