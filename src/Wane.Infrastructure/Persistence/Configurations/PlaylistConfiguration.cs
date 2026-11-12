using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wane.Domain.Entities;

namespace Wane.Infrastructure.Persistence.Configurations;

public class PlaylistConfiguration : IEntityTypeConfiguration<Playlist>
{
    public void Configure(EntityTypeBuilder<Playlist> builder)
    {
        builder.ToTable("playlists");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.Property(p => p.CreatedAt).IsRequired();
        builder.Property(p => p.UpdatedAt);

        builder
            .HasOne(p => p.Profile)
            .WithMany(p => p.Playlists)
            .HasForeignKey(p => p.ProfileId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}
