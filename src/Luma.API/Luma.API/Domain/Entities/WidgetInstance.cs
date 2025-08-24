namespace Luma.API.Domain.Entities;

public class WidgetInstance
{
    public Guid Id { get; set; }                          // PK
    public Guid DashboardId { get; set; }                 // FK -> dashboards.id
    public Guid WidgetId { get; set; }                    // FK -> widgets.id

    // default grid placement (you can override per breakpoint in config if desired)
    public int X { get; set; } = 0;
    public int Y { get; set; } = 0;
    public int W { get; set; } = 4;
    public int H { get; set; } = 3;

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    // nav
    public Dashboard Dashboard { get; set; } = null!;
    public Widget Widget { get; set; } = null!;
    public WidgetConfig? Config { get; set; }             // 1:1
}
