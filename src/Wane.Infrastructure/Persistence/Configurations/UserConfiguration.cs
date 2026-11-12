namespace Wane.Infrastructure.Persistence.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wane.Domain.Entities;

/// <summary>
/// Maps <see cref="User"/> to the <c>users</c> table.
/// </summary>
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    /// <summary>
    /// Sets the table, the key, a unique index on the username and the column rules
    /// (required username of up to 100 characters, required password hash of up to 256).
    /// </summary>
    /// <param name="builder">The builder of the entity.</param>
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.HasKey(p => p.Id);
        builder.HasIndex(p => p.Username).IsUnique();
        builder.Property(p => p.Username).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Password).IsRequired().HasMaxLength(256);
        builder.Property(p => p.CreatedAt).IsRequired();
        builder.Property(p => p.UpdatedAt);
    }
}
