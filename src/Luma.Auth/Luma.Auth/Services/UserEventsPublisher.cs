using Luma.ServiceBus.Contracts.Events;
using Luma.ServiceBus.Configuration;
using Luma.ServiceBus.Publishers;
using Microsoft.Extensions.Options;

namespace Luma.Auth.Services
{
    public interface IUserEventsPublisher
    {
        Task PublishUserCreatedAsync(UserCreatedEvent evt, CancellationToken ct = default);
    }

    public class UserEventsPublisher : IUserEventsPublisher
    {
        private readonly IEventPublisher _publisher;
        private readonly NatsOptions _options;

        public UserEventsPublisher(IEventPublisher publisher, IOptions<NatsOptions> options)
        {
            _publisher = publisher;
            _options = options.Value;
        }

        public async Task PublishUserCreatedAsync(UserCreatedEvent evt, CancellationToken ct = default)
        {
            await _publisher.PublishAsync(_options.Subjects.UserCreated, evt, ct);
        }
    }
}
