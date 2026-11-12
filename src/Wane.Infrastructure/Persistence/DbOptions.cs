namespace Wane.Infrastructure.Persistence;

using System.ComponentModel.DataAnnotations;

public class DbOptions
{
    [Required]
    public string Host { get; set; } = "";

    [Range(1, 65535)]
    public int Port { get; set; }

    [Required]
    public string DbName { get; set; } = "";

    [Required]
    public string Username { get; set; } = "";

    [Required]
    public string Password { get; set; } = "";
}
