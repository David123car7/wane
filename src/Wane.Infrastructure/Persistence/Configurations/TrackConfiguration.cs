using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wane.Domain.Entities;

namespace Wane.Infrastructure.Persistence.Configurations;

public class TrackConfiguration : IEntityTypeConfiguration<Track>
{
    public void Configure(EntityTypeBuilder<Track> builder)
    {
        builder.ToTable("tracks");
        builder.HasKey(p => p.Id);
        builder.HasIndex(p => p.Tittle).IsUnique();
        builder.Property(p => p.Tittle).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Genre).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Artist).HasMaxLength(100);
        builder.Property(p => p.Year);
        builder.Property(p => p.CreatedAt).IsRequired();
        builder.Property(p => p.UpdatedAt);
    }
}
