namespace Luma.ServiceBus.Subscribers
{
    public interface IEventSubscriber
    {
        Task SubscribeAsync<TEvent>(string subject, Func<TEvent, CancellationToken, Task> handler, CancellationToken ct = default) where TEvent : class;
    }
}