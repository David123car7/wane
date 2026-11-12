using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wane.Domain.Entities;

namespace Wane.Infrastructure.Persistence.Configurations;

public class TrackFileConfiguration : IEntityTypeConfiguration<TrackFile>
{
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
