using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Npgsql;
using Wane.Infrastructure.Persistence;

namespace Wane.Infrastructure;

public static class DependencyInjection
{
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
