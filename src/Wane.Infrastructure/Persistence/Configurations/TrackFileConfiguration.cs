using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wane.Domain.Entities;

namespace Wane.Infrastructure.Persistence.Configurations;

/// <summary>
/// Maps <see cref="TrackFile"/> to the <c>track_files</c> table.
/// </summary>
public class TrackFileConfiguration : IEntityTypeConfiguration<TrackFile>
{
    /// <summary>
    /// Sets the table, the key, a unique index on the storage key, the column rules
    /// (required storage key of up to 200 characters, required format of up to 5)
    /// and the required relation to the track, which cannot be deleted while it has files.
    /// </summary>
    /// <param name="builder">The builder of the entity.</param>
    public void Configure(EntityTypeBuilder<TrackFile> builder)
    {
        builder.ToTable("track_files");
        builder.HasKey(p => p.Id);
        builder.HasIndex(p => p.StorageKey).IsUnique();
        builder.Property(p => p.StorageKey).IsRequired().HasMaxLength(200);
        builder.Property(p => p.Format).IsRequired().HasMaxLength(5);
        builder.Property(p => p.CreatedAt).IsRequired();
        builder.Property(p => p.UpdatedAt);

        builder
            .HasOne(p => p.Track)
            .WithMany(p => p.TrackFiles)
            .HasForeignKey(p => p.TrackId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}
