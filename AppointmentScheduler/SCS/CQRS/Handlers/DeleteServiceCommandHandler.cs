using MediatR;
using SCS.CQRS.Commands;
using SCS.Data;
using SharedLibrary.CQRS.Handlers;

namespace SCS.CQRS.Handlers
{
    public class DeleteServiceCommandHandler : CommandHandler<DeleteServiceCommand, Unit>
    {
        private readonly IServiceRepository _serviceRepository;

        public DeleteServiceCommandHandler(IServiceRepository serviceRepository, ILogger<DeleteServiceCommandHandler> logger)
            : base(logger)
        {
            _serviceRepository = serviceRepository;
        }

        public override async Task<Unit> Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
        {
            await _serviceRepository.DeleteAsync(request.Id);
            return Unit.Value;
        }
    }
}
