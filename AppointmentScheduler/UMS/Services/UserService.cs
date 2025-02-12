using CommonBase.Interfaces;
using CommonBase.Models;
using MediatR;
using UMS.CQRS.Commands;
using UMS.Events;
using UMS.Interfaces;

namespace UMS.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMediator _mediator;
        private readonly IMessageBus _messageBus;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, IMediator mediator, IMessageBus messageBus, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _mediator = mediator;
            _messageBus = messageBus;
            _logger = logger;
        }

        public async Task<Guid> RegisterUser(User user)
        {
            var createUserCommand = new CreateUserCommand
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };

            var userId = await _mediator.Send(createUserCommand);

            // Publish UserRegisteredEvent
            var userRegisteredEvent = new UserRegisteredEvent
            {
                UserId = userId,
                Email = user.Email
            };
            await _messageBus.PublishAsync(userRegisteredEvent);

            return userId;
        }

        // ... other user-related methods
    }
}
