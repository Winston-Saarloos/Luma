using NATS.Client.JetStream;

namespace Luma.ServiceBus.Infrastructure
{
    public class StreamManager
    {
        private readonly NatsJSContext _js;
        private readonly string _stream;

        public StreamManager(NatsJSContext js, string stream)
        {
            _js = js;
            _stream = stream;
        }

        public async Task EnsureStreamAsync(string[] subjects, CancellationToken ct = default)
        {
            try
            {
                await _js.GetStreamAsync(_stream, cancellationToken: ct);
            }
            catch
            {
                await _js.CreateStreamAsync(
                    new NATS.Client.JetStream.Models.StreamConfig(name: _stream, subjects: subjects),
                    ct
                );
            }
        }
    }
}
