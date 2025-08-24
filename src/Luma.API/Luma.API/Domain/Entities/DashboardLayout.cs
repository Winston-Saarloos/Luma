namespace Luma.API.Domain.Entities;
public class DashboardLayout
{
    public Guid Id { get; set; }                          // PK
    public Guid DashboardId { get; set; }                 // FK -> dashboards.id
    public Breakpoint Bp { get; set; } = Breakpoint.Lg;   // 'lg','md','sm','xs'
    public int GridCols { get; set; } = 24;               // react-grid-layout columns
    public int RowHeight { get; set; } = 60;              // px per row

    // nav
    public Dashboard Dashboard { get; set; } = null!;
}