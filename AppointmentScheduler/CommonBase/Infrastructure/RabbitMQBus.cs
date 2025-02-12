using CommonBase.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace CommonBase.Infrastructure
{
    public class RabbitMQBus : IMessageBus
    {
        private readonly IPublishEndpoint _publishEndpoint;
        //private readonly ISubscribeEndpoint _subscribeEndpoint;
        private readonly IBus _bus;
        private readonly ILogger<RabbitMQBus> _logger;

        public RabbitMQBus(IPublishEndpoint publishEndpoint, IBus bus, ILogger<RabbitMQBus> logger)
        {
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
            //_subscribeEndpoint = subscribeEndpoint ?? throw new ArgumentNullException(nameof(subscribeEndpoint));
            _bus = bus ?? throw new ArgumentNullException(nameof(bus));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task PublishAsync<T>(T message) where T : class
        {
            try
            {
                await _publishEndpoint.Publish(message);
                _logger.LogInformation($"Published message of type '{typeof(T).Name}'.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to publish message.");
                throw;
            }
        }

        public async Task SubscribeAsync<T>(string queueName, Func<T, CancellationToken, Task> handler) where T : class
        {
           try
            {
                _bus.ConnectReceiveEndpoint(queueName, e =>
                {
                    e.Handler<T>(async context =>
                    {
                        await handler(context.Message, context.CancellationToken);
                    });
                });

                _logger.LogInformation($"Subscribed to messages of type '{typeof(T).Name}' on queue '{queueName}'.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to subscribe to messages of type '{typeof(T).Name}'.");
                throw;
            }
        }
    }
}