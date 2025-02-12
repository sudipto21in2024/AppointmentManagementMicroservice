using MediatR;
using SharedLibrary.CQRS.Handlers;
using UMS.CQRS.Commands;
using UMS.Interfaces;

namespace UMS.CQRS.Handlers
{
    public class DeleteUserCommandHandler : CommandHandler<DeleteUserCommand, Unit>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository, ILogger<DeleteUserCommandHandler> logger)
            : base(logger)
        {
            _userRepository = userRepository;
        }

        public override async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            await _userRepository.DeleteAsync(request.UserId);
            _logger.LogInformation($"Deleted user with ID: {request.UserId} - CorrelationId: {request.CorrelationId}");
            return Unit.Value;
        }
    }
}
