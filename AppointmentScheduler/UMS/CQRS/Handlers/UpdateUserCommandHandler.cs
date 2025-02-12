using MediatR;
using SharedLibrary.CQRS.Handlers;
using UMS.CQRS.Commands;
using UMS.Interfaces;

namespace UMS.CQRS.Handlers
{
    public class UpdateUserCommandHandler : CommandHandler<UpdateUserCommand, Unit>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserCommandHandler(IUserRepository userRepository, ILogger<UpdateUserCommandHandler> logger)
            : base(logger)
        {
            _userRepository = userRepository;
        }

       

        public override async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null)
            {
                _logger.LogWarning($"User with ID: {request.UserId} not found.");
                return Unit.Value;
            }

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;

            await _userRepository.UpdateAsync(user);
            _logger.LogInformation($"Updated user with ID: {request.UserId} - CorrelationId: {request.CorrelationId}");
            return Unit.Value;
        }
    }
}
