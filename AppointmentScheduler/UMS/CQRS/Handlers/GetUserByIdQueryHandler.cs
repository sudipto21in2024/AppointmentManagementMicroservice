using CommonBase.Models;
using SharedLibrary.CQRS.Handlers;
using UMS.CQRS.Queries;
using UMS.Interfaces;

namespace UMS.CQRS.Handlers
{
    public class GetUserByIdQueryHandler : QueryHandler<GetUserByIdQuery, User>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryHandler(IUserRepository userRepository, ILogger<GetUserByIdQueryHandler> logger)
            : base(logger)
        {
            _userRepository = userRepository;
        }

        
        public override async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Handling GetUserByIdQuery - UserId: {request.UserId} - CorrelationId: {request.CorrelationId}");
            return await _userRepository.GetByIdAsync(request.UserId);
        }
    }
}
