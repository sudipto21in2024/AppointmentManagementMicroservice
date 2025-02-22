using CommonBase.Data;
using CommonBase.DTO;
using SCS.CQRS.Queries;
using SCS.Data;
using SharedLibrary.CQRS.Handlers;

namespace ServiceCatalogService.CQRS.Handlers
{
        public class GetServiceDropdownQueryHandler : QueryHandler<GetServiceDropdownQuery, List<ServiceDropdownDTO>>
        {
            private readonly IServiceRepository _serviceRepository;

            public GetServiceDropdownQueryHandler(IServiceRepository serviceRepository, ILogger<GetServiceDropdownQueryHandler> logger)
                : base(logger)
            {
                _serviceRepository = serviceRepository;
            }

            public override async Task<List<ServiceDropdownDTO>> Handle(GetServiceDropdownQuery request, CancellationToken cancellationToken)
            {
                // Assuming you have a repository method that fetches active services for the dropdown
                return await _serviceRepository.GetActiveServiceDropdownAsync();
            }
        }
 }
