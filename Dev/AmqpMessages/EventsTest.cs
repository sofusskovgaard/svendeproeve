using App.Infrastructure.Extensions;
using App.Infrastructure.Options;
using App.Services.Orders.Infrastructure.Events;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace AmqpMessages
{
    public class EventsTest
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public EventsTest()
        {
            var services = new ServiceCollection();

            Environment.SetEnvironmentVariable("RABBITMQ_HOST", "localhost");

            services.AddOptions<RabbitMQOptions>();
            services.AddRabbitMq();

            var provider = services.BuildServiceProvider();

            _publishEndpoint = provider.GetRequiredService<IPublishEndpoint>();
        }

        [Fact]
        public async Task ProductCreatedEventMessage_Test()
        {
            await _publishEndpoint.Publish(new ProductCreatedEventMessage()
            {
                ProductId = "63ff13a56f4f5d977de41e2a",
                Name = "Test product",
                Description = "A very nice test product description",
                Price = 99.99M
            });
        }
    }
}