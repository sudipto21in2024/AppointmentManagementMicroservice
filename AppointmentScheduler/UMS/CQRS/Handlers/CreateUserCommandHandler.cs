using CommonBase.Models;
using SharedLibrary.CQRS.Handlers;
using UMS.CQRS.Commands;
using UMS.Interfaces;

namespace UMS.CQRS.Handlers
{
    public class CreateUserCommandHandler : CommandHandler<CreateUserCommand, Guid>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUserRepository userRepository, ILogger<CreateUserCommandHandler> logger)
            : base(logger)
        {
            _userRepository = userRepository;
        }

        //public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        //{
           
        //}

        public override async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email
            };

           await _userRepository.CreateAsync(user);
            _logger.LogInformation($"Created user with ID: {user.Id} - CorrelationId: {request.CorrelationId}");
            return user.Id;
        }
    }
}
