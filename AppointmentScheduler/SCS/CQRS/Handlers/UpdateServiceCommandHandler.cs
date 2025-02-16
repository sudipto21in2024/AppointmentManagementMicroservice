using MediatR;
using SCS.CQRS.Commands;
using SCS.Data;
using SharedLibrary.CQRS.Handlers;

namespace SCS.CQRS.Handlers
{
    public class UpdateServiceCommandHandler : CommandHandler<UpdateServiceCommand, Unit>
    {
        private readonly IServiceRepository _serviceRepository;

        public UpdateServiceCommandHandler(IServiceRepository serviceRepository, ILogger<UpdateServiceCommandHandler> logger)
          : base(logger)
        {
            _serviceRepository = serviceRepository;
        }

        public override async Task<Unit> Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
        {
            var existingService = await _serviceRepository.GetByIdAsync(request.Id);

            if (existingService == null)
            {
                _logger.LogWarning($"Service with ID: {request.Id} not found.");
                return Unit.Value;
            }

            existingService.Name = request.Name;
            existingService.Description = request.Description;
            existingService.Price = request.Price;
            existingService.DurationInMinutes = request.DurationInMinutes;
            existingService.CategoryId = request.CategoryId;

            await _serviceRepository.UpdateAsync(existingService);
            return Unit.Value;
        }
    }
}
