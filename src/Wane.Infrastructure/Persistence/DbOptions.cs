namespace Wane.Infrastructure.Persistence;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// Settings to connect to PostgreSQL, bound from the <c>Database</c> configuration section
/// and validated at startup.
/// </summary>
public class DbOptions
{
    /// <summary>Host name or address of the database server.</summary>
    [Required]
    public string Host { get; set; } = "";

    /// <summary>TCP port of the database server, from 1 to 65535.</summary>
    [Range(1, 65535)]
    public int Port { get; set; }

    /// <summary>Name of the database.</summary>
    [Required]
    public string DbName { get; set; } = "";

    /// <summary>User name used to connect.</summary>
    [Required]
    public string Username { get; set; } = "";

    /// <summary>Password used to connect. A secret, never kept in committed files.</summary>
    [Required]
    public string Password { get; set; } = "";
}
