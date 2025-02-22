using CommonBase.DTO;
using CommonBase.Models;
using SCS.CQRS.Queries;
using SCS.Data;
using SharedLibrary.CQRS.Handlers;

namespace SCS.CQRS.Handlers
{
    public class GetAllServicesQueryHandler : QueryHandler<GetAllServicesQuery, ServiceResponseDTO>
    {
        private readonly IServiceRepository _serviceRepository;

        public GetAllServicesQueryHandler(IServiceRepository serviceRepository, ILogger<GetAllServicesQueryHandler> logger)
            : base(logger)
        {
            _serviceRepository = serviceRepository;
        }

        public override async Task<ServiceResponseDTO> Handle(GetAllServicesQuery request, CancellationToken cancellationToken)
        {
            // Adapt your repository method to use the query parameters
            var services = await _serviceRepository.GetAllServicesAsync(
                request.PageIndex,
                request.PageSize,
                request.Keyword,
                request.CategoryId,
                request.ProviderId
                );

            return services; // The repository method already returns ServiceResponseDTO
        }
    }
}
