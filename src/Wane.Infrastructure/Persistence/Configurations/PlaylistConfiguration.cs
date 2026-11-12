using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wane.Domain.Entities;

namespace Wane.Infrastructure.Persistence.Configurations;

/// <summary>
/// Maps <see cref="Playlist"/> to the <c>playlists</c> table.
/// </summary>
public class PlaylistConfiguration : IEntityTypeConfiguration<Playlist>
{
    /// <summary>
    /// Sets the table, the key, the column rules (required name of up to 100 characters)
    /// and the required relation to the profile, which cannot be deleted while it has playlists.
    /// </summary>
    /// <param name="builder">The builder of the entity.</param>
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
