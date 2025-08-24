namespace Luma.API.Domain.Entities;

public class Widget
{
    public Guid Id { get; set; }                          // PK
    public string TypeKey { get; set; } = null!;          // e.g. "weather", "notes" (unique)
    public string DisplayName { get; set; } = null!;
}