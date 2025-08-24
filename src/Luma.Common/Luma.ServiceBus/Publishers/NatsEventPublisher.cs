using Luma.ServiceBus.Configuration;
using Luma.ServiceBus.Infrastructure;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NATS.Client.Core;
using NATS.Client.JetStream;
using System.Text.Json;

namespace Luma.ServiceBus.Publishers
{
    public class NatsEventPublisher : IEventPublisher, IAsyncDisposable
    {
        private readonly NatsJSContext _js;
        private readonly NatsConnection _connection;
        private readonly StreamManager _streamManager;
        private readonly string _stream;
        private readonly ILogger<NatsEventPublisher> _logger;

        public NatsEventPublisher(INatsConnectionFactory connectionFactory, IOptions<NatsOptions> options, ILogger<NatsEventPublisher> logger)
        {
            var optionsValue = options.Value;
            _logger = logger;
            _connection = connectionFactory.CreateConnection();
            _js = new NatsJSContext(_connection);
            _stream = optionsValue.Stream;
            _streamManager = new StreamManager(_js, _stream);
        }

        public async Task PublishAsync<TEvent>(string subject, TEvent evt, CancellationToken ct = default) where TEvent : class
        {
            try
            {
                _logger.LogDebug("Publishing event to subject {subject}", subject);

                // Ensure stream exists with the subject
                await _streamManager.EnsureStreamAsync(new[] { subject }, ct);

                var payload = JsonSerializer.SerializeToUtf8Bytes(evt);
                var pubAck = await _js.PublishAsync(subject, payload, cancellationToken: ct);

                _logger.LogInformation("Successfully published event - Subject: {subject}, Stream: {stream}, Sequence: {sequence}",
                    subject, pubAck.Stream, pubAck.Seq);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to publish event to subject {subject}", subject);
                throw;
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_connection != null)
            {
                await _connection.DisposeAsync();
            }
        }
    }
}