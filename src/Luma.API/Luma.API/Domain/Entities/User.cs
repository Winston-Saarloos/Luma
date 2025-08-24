namespace Luma.API.Domain.Entities;

public class User
{
    public Guid Id { get; set; }                          // PK
    public string Email { get; set; } = null!;            // unique
    public string DisplayName { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTime? LastLoggedInAt { get; set; }

    // nav
    public ICollection<Dashboard> Dashboards { get; set; } = new List<Dashboard>();
}
