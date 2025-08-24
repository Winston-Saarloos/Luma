namespace Luma.API.Domain.Entities;
public class Dashboard
{
    public Guid Id { get; set; }                          // PK
    public Guid UserId { get; set; }                      // FK -> users.id
    public string Name { get; set; } = "Main";
    public int SortOrder { get; set; } = 0;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    // nav
    public User User { get; set; } = null!;
    public ICollection<DashboardLayout> Layouts { get; set; } = new List<DashboardLayout>();
    public ICollection<WidgetInstance> WidgetInstances { get; set; } = new List<WidgetInstance>();
}

