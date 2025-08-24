using Luma.API.Domain.Entities;
using Luma.API.Infrastructure;
using Luma.ServiceBus.Configuration;
using Luma.ServiceBus.Contracts.Events;
using Luma.ServiceBus.Subscribers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Luma.API.Services.EventBus
{
    public class UserCreatedSubscriber : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IEventSubscriber _subscriber;
        private readonly NatsOptions _options;

        public UserCreatedSubscriber(
            IServiceScopeFactory scopeFactory, 
            IEventSubscriber subscriber,
            IOptions<NatsOptions> options)
        {
            _scopeFactory = scopeFactory;
            _subscriber = subscriber;
            _options = options.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine($"=== STARTING USER CREATED SUBSCRIBER ===");
            Console.WriteLine($"Subject: {_options.Subjects.UserCreated}");

            try
            {
                await _subscriber.SubscribeAsync<UserCreatedEvent>(
                    _options.Subjects.UserCreated,
                    HandleUserCreatedEvent,
                    stoppingToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"=== SUBSCRIBER FAILED TO START ===");
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw;
            }
        }

        private async Task HandleUserCreatedEvent(UserCreatedEvent evt, CancellationToken ct)
        {
            Console.WriteLine($"=== PROCESSING USER CREATED EVENT ===");
            Console.WriteLine($"User ID: {evt.UserId}");
            Console.WriteLine($"Email: {evt.Email}");

            await UpsertUserAsync(evt, ct);
            Console.WriteLine($"=== USER UPSERTED SUCCESSFULLY ===");
        }

        private async Task UpsertUserAsync(UserCreatedEvent evt, CancellationToken ct)
        {
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<LumaDbContext>();

            var existing = await db.Users.FirstOrDefaultAsync(u => u.Id == evt.UserId, ct);
            if (existing is null)
            {
                db.Users.Add(new User
                {
                    Id = evt.UserId,
                    Email = evt.Email,
                    DisplayName = evt.DisplayName,
                    CreatedAt = evt.CreatedAtUtc
                });
            }
            else
            {
                existing.Email = evt.Email;
                existing.DisplayName = evt.DisplayName;
            }
            await db.SaveChangesAsync(ct);
        }
    }
}
