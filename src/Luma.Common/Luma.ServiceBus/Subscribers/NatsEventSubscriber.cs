using Luma.ServiceBus.Configuration;
using Luma.ServiceBus.Infrastructure;
using Microsoft.Extensions.Options;
using NATS.Client.Core;
using NATS.Client.JetStream;
using NATS.Client.JetStream.Models;
using System.Text.Json;

namespace Luma.ServiceBus.Subscribers
{
    public class NatsEventSubscriber : IEventSubscriber
    {
        private readonly NatsJSContext _js;
        private readonly StreamManager _streamManager;
        private readonly string _stream;
        private readonly NatsConnection _connection;

        public NatsEventSubscriber(INatsConnectionFactory connectionFactory, IOptions<NatsOptions> options)
        {
            var optionsValue = options.Value;
            _connection = connectionFactory.CreateConnection();
            _js = new NatsJSContext(_connection);
            _stream = optionsValue.Stream;
            _streamManager = new StreamManager(_js, _stream);
        }

        public async Task SubscribeAsync<TEvent>(string subject, Func<TEvent, CancellationToken, Task> handler, CancellationToken ct = default) where TEvent : class
        {
            Console.WriteLine($"=== SUBSCRIBER INITIALIZING ===");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Stream: {_stream}");

            try
            {
                // Ensure stream exists
                Console.WriteLine($"=== ENSURING STREAM EXISTS ===");
                await _streamManager.EnsureStreamAsync(new[] { subject }, ct);
                Console.WriteLine($"=== STREAM READY ===");

                // Create an ephemeral consumer for continuous listening
                Console.WriteLine($"=== CREATING CONSUMER ===");
                var consumerCfg = new ConsumerConfig();
                
                var consumer = await _js.CreateOrUpdateConsumerAsync(_stream, consumerCfg, cancellationToken: ct);
                Console.WriteLine($"=== CONSUMER CREATED ===");

                Console.WriteLine($"=== SUBSCRIBER READY ===");
                Console.WriteLine($"Subject: {subject}");
                Console.WriteLine($"Consumer: {consumer.Info.Name}");
                Console.WriteLine($"Durable: {consumer.Info.Config.DurableName}");

                // Start consuming messages continuously using ConsumeAsync
                Console.WriteLine($"=== STARTING MESSAGE CONSUMPTION ===");
                await foreach (var msg in consumer.ConsumeAsync<ReadOnlyMemory<byte>>(cancellationToken: ct))
            {
                try
                {
                    Console.WriteLine($"=== RECEIVED MESSAGE ===");
                    Console.WriteLine($"Subject: {msg.Subject}");
                    Console.WriteLine($"Data length: {msg.Data.Length}");
                    Console.WriteLine($"Data: {System.Text.Encoding.UTF8.GetString(msg.Data.Span)}");

                    var evt = JsonSerializer.Deserialize<TEvent>(msg.Data.Span);
                    if (evt != null)
                    {
                        await handler(evt, ct);
                        await msg.AckAsync(cancellationToken: ct);
                        Console.WriteLine($"=== MESSAGE PROCESSED AND ACKED ===");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"=== MESSAGE PROCESSING FAILED ===");
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine($"Stack trace: {ex.StackTrace}");
                    await msg.NakAsync(cancellationToken: ct);
                }
            }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"=== SUBSCRIBER INITIALIZATION FAILED ===");
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
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