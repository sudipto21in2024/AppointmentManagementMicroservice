using CommonBase.Models;
using Grpc.Core;
using SCS.Data;
using ServiceCatalog.Grpc;
using Service = ServiceCatalog.Grpc.Service;

namespace ServiceCatalogService.Grpc
{
    public class ServiceCatalogGrpc : ServiceGRPC.ServiceGRPCBase
    {
        private readonly IServiceRepository _serviceRepository; // Your repository
        private readonly ILogger<ServiceCatalogGrpc> _logger;

        public ServiceCatalogGrpc(IServiceRepository serviceRepository, ILogger<ServiceCatalogGrpc> logger)
        {
            _serviceRepository = serviceRepository;
            _logger = logger;
        }

        public override async Task<Service> GetService(GetServiceRequest request, ServerCallContext context)
        {
            if (!Guid.TryParse(request.Id, out var serviceId))
            {
                _logger.LogError($"Invalid service ID: {request.Id}");
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid service ID."));
            }

            var servicedata = await _serviceRepository.GetServiceWithProviderAsync(serviceId); // Use your repository method

            if (servicedata == null)
            {
                _logger.LogWarning($"Service with ID {request.Id} not found.");
                throw new RpcException(new Status(StatusCode.NotFound, "Service not found."));
            }

            // Map your domain Service object to the gRPC Service message
            var grpcService = new Service // gRPC Service message
            {
                Id = servicedata.Id.ToString(),
                Name = servicedata.Name,
                Description = servicedata.Description,
                Price = servicedata.Price.ToString(), // Convert decimal to double for gRPC
                ProviderName = servicedata.ProviderName, // Map provider name
                ProviderEmail = servicedata.ProviderEmail, // Map provider email
                // ... map other properties
            };

            return grpcService;
        }
    }
}
