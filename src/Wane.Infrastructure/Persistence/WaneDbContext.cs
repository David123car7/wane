using Microsoft.EntityFrameworkCore;
using Wane.Domain.Entities;

namespace Wane.Infrastructure.Persistence;

public class WaneDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Profile> Profiles { get; set; }

    public WaneDbContext(DbContextOptions<WaneDbContext> options)
        : base(options) { }
}
