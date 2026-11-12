using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Npgsql;
using Wane.Infrastructure.Persistence;

namespace Wane.Infrastructure;

/// <summary>
/// Registration of the Infrastructure services, called by the host.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registers the database options and the <see cref="WaneDbContext"/>.
    /// The options are read from the <c>Database</c> configuration section and validated at startup.
    /// </summary>
    /// <param name="services">The service collection to add the services to.</param>
    /// <param name="configuration">The application configuration.</param>
    /// <returns>The same service collection, to allow chaining.</returns>
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services
            .AddOptions<DbOptions>()
            .Bind(configuration.GetSection("Database"))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddDbContext<WaneDbContext>(ConfigureDb);

        return services;
    }

    /// <summary>
    /// Builds the PostgreSQL connection string from the <see cref="DbOptions"/> and configures the context to use it.
    /// </summary>
    /// <param name="serviceProvider">Provider used to resolve the database options.</param>
    /// <param name="options">The options builder of the context.</param>
    private static void ConfigureDb(
        IServiceProvider serviceProvider,
        DbContextOptionsBuilder options
    )
    {
        var db = serviceProvider.GetRequiredService<IOptions<DbOptions>>().Value;
        string connectionString = new NpgsqlConnectionStringBuilder
        {
            Host = db.Host,
            Port = db.Port,
            Database = db.DbName,
            Username = db.Username,
            Password = db.Password,
        }.ConnectionString;

        options.UseNpgsql(connectionString);
    }
}
