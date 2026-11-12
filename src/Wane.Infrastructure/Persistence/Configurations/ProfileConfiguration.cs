namespace Wane.Infrastructure.Persistence.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wane.Domain.Entities;

/// <summary>
/// Maps <see cref="Profile"/> to the <c>profiles</c> table.
/// </summary>
public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
{
    /// <summary>
    /// Sets the table, the key, a unique index on the name, the column rules
    /// (required name of up to 100 characters, code of up to 4) and the required relation to the user,
    /// which cannot be deleted while it has profiles.
    /// </summary>
    /// <param name="builder">The builder of the entity.</param>
    public void Configure(EntityTypeBuilder<Profile> builder)
    {
        builder.ToTable("profiles");
        builder.HasKey(p => p.Id);
        builder.HasIndex(p => p.Name).IsUnique();
        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Code).HasMaxLength(4);
        builder.Property(p => p.CreatedAt).IsRequired();
        builder.Property(p => p.UpdatedAt);

        builder
            .HasOne(p => p.User)
            .WithMany(p => p.Profiles)
            .HasForeignKey(p => p.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}
