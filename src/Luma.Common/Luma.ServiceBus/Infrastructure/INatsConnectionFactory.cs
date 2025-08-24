using NATS.Client.Core;
using NATS.Client.JetStream;

namespace Luma.ServiceBus.Infrastructure
{
    public interface INatsConnectionFactory
    {
        NatsConnection CreateConnection();
        NatsJSContext CreateJetStreamContext();
    }
}
