namespace Wane.Infrastructure.Persistence.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wane.Domain.Entities;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
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
