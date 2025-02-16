using CommonBase.Models;
using SCS.CQRS.Queries;
using SCS.Data;
using SharedLibrary.CQRS.Handlers;

namespace SCS.CQRS.Handlers
{
    public class GetAllServicesQueryHandler : QueryHandler<GetAllServicesQuery, IEnumerable<Service>>
    {
        private readonly IServiceRepository _serviceRepository;

        public GetAllServicesQueryHandler(IServiceRepository serviceRepository, ILogger<GetAllServicesQueryHandler> logger)
          : base(logger)
        {
            _serviceRepository = serviceRepository;
        }

        public override async Task<IEnumerable<Service>> Handle(GetAllServicesQuery request, CancellationToken cancellationToken)
        {
            return await _serviceRepository.GetAllAsync();
        }
    }
}
