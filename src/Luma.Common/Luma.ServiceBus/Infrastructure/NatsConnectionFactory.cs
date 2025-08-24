using Luma.ServiceBus.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NATS.Client.Core;
using NATS.Client.JetStream;

namespace Luma.ServiceBus.Infrastructure
{
    public class NatsConnectionFactory : INatsConnectionFactory 
    {
        private readonly NatsOptions _options;
        private readonly ILogger<NatsConnectionFactory> _logger;

        public NatsConnectionFactory(IOptions<NatsOptions> options, ILogger<NatsConnectionFactory> logger)
        {
            _options = options.Value;
            _logger = logger;
        }

        public NatsConnection CreateConnection()
        {
            _logger.LogInformation("Creating NATS connection to {Url}", _options.Url);

            var natsOpts = NatsOpts.Default with 
            { 
                Url = _options.Url,
                ConnectTimeout =  TimeSpan.FromSeconds(5),
                ReconnectWaitMax = TimeSpan.FromSeconds(1),
                MaxReconnectRetry = -1,
                ReconnectJitter = TimeSpan.FromMilliseconds(100),
            };

            var connection = new NatsConnection(natsOpts);

            _logger.LogInformation("NATS connection created successfully");

            return connection;
        }

        public NatsJSContext CreateJetStreamContext()
        {
            var connection = CreateConnection();
            return new NatsJSContext(connection);
        }
    }
}
