using System.Text.Json;

namespace Luma.API.Domain.Entities;
public class WidgetConfig
{
    // PK = FK (1:1 with WidgetInstance)
    public Guid WidgetInstanceId { get; set; }
    public JsonDocument Config { get; set; } = JsonDocument.Parse("{}"); // JSONB in PG

    // nav
    public WidgetInstance WidgetInstance { get; set; } = null!;
}
