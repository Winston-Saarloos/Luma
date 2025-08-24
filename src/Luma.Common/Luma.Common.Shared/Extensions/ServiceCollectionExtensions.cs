using Luma.ServiceBus.Configuration;
using Luma.ServiceBus.Infrastructure;
using Luma.ServiceBus.Publishers;
using Luma.ServiceBus.Subscribers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Luma.Common.Shared.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLumaServiceBus(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<NatsOptions>(configuration.GetSection("Nats"));
            services.AddSingleton<INatsConnectionFactory, NatsConnectionFactory>();
            services.AddSingleton<IEventPublisher, NatsEventPublisher>();
            services.AddSingleton<IEventSubscriber, NatsEventSubscriber>();

            return services;
        }
    }
}
