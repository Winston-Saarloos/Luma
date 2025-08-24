namespace Luma.ServiceBus.Publishers
{
    public interface IEventPublisher
    {
        Task PublishAsync<TEvent>(string subject, TEvent evt, CancellationToken ct = default) where TEvent : class;
    }
}
