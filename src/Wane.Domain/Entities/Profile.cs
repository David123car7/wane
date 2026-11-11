namespace Wane.Domain.Entities;

/// <summary>
/// Profile entity, it will have a name and a opcional security code;
/// </summary>
public class Profile
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
}
