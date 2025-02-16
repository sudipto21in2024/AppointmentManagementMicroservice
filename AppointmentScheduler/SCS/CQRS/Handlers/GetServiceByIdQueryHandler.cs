using CommonBase.Models;
using SCS.CQRS.Queries;
using SCS.Data;
using SharedLibrary.CQRS.Handlers;

namespace SCS.CQRS.Handlers
{
    public class GetServiceByIdQueryHandler : QueryHandler<GetServiceByIdQuery, Service>
    {
        private readonly IServiceRepository _serviceRepository;

        public GetServiceByIdQueryHandler(IServiceRepository serviceRepository, ILogger<GetServiceByIdQueryHandler> logger)
          : base(logger)
        {
            _serviceRepository = serviceRepository;
        }

        public override async Task<Service> Handle(GetServiceByIdQuery request, CancellationToken cancellationToken)
        {
            return await _serviceRepository.GetByIdAsync(request.Id);
        }
    }
}
