using CommonBase.Interfaces;
using MassTransit;
using MediatR;
using UMS.Events;

namespace UMS.EventHandlers
{
    public class UserRegisteredEventHandler : INotificationHandler<UserRegisteredEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<UserRegisteredEventHandler> _logger;

        public UserRegisteredEventHandler(IPublishEndpoint publishEndpoint, ILogger<UserRegisteredEventHandler> logger)
        {
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        public async Task Handle(UserRegisteredEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                // Publish the UserRegisteredEvent
                await _publishEndpoint.Publish(notification, cancellationToken);
                _logger.LogInformation($"Published UserRegisteredEvent for user with ID: {notification.UserId}");

                // Publish another event to a different queue (if needed)
                // 
                // This line is commented out because the IPublishEndpoint 
                // does not have an overload that accepts a string as the first argument.
                // await _messageBus.PublishAsync("user_activity_log", new UserActivityLogEvent { UserId = notification.UserId, Action = "Registered" }); 

                // To publish to a specific exchange or queue, you can use the following:
                // await _publishEndpoint.Publish<UserActivityLogEvent>(new UserActivityLogEvent { UserId = notification.UserId, Action = "Registered" }, 
                //     context => context.Use("user_activity_log_exchange")); 

                // Or, if you have a dedicated endpoint for this event:
                // await _publishEndpoint.Publish<UserActivityLogEvent>(new UserActivityLogEvent { UserId = notification.UserId, Action = "Registered" }, 
                //     context => context.UseEndpoint("user_activity_log_endpoint"));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to publish UserRegisteredEvent.", ex);
                // Handle the exception appropriately (e.g., retry mechanism)
            }
        }
    }
}
