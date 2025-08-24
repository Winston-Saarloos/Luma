namespace Luma.ServiceBus.Contracts.Events
{
    public sealed record UserCreatedEvent(
        Guid UserId,
        string Email,
        string DisplayName,
        DateTime CreatedAtUtc
    );
}
