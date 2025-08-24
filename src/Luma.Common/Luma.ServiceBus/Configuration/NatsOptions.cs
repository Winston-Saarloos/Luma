namespace Luma.ServiceBus.Configuration
{
    public class NatsOptions
    {
        public string Url { get; set; } = "nats://localhost:4222";
        public string Stream { get; set; } = "USER";
        public SubjectsOptions Subjects { get; set; } = new();
        public ConsumerOptions Consumer { get; set; } = new();

        public class SubjectsOptions
        {
            public string UserCreated { get; set; } = "luma.auth.user.created";
        }

        public class  ConsumerOptions
        {
            public string Durable { get; set; } = "api-user-sync";
        }
    }
}
