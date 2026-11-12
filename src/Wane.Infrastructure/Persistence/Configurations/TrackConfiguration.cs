using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wane.Domain.Entities;

namespace Wane.Infrastructure.Persistence.Configurations;

/// <summary>
/// Maps <see cref="Track"/> to the <c>tracks</c> table.
/// </summary>
public class TrackConfiguration : IEntityTypeConfiguration<Track>
{
    /// <summary>
    /// Sets the table, the key, a unique index on the title and the column rules
    /// (required title and genre of up to 100 characters, optional artist of up to 100).
    /// </summary>
    /// <param name="builder">The builder of the entity.</param>
    public void Configure(EntityTypeBuilder<Track> builder)
    {
        builder.ToTable("tracks");
        builder.HasKey(p => p.Id);
        builder.HasIndex(p => p.Title).IsUnique();
        builder.Property(p => p.Title).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Genre).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Artist).HasMaxLength(100);
        builder.Property(p => p.Year);
        builder.Property(p => p.CreatedAt).IsRequired();
        builder.Property(p => p.UpdatedAt);
    }
}
