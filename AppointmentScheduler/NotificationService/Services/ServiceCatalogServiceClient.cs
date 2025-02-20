using CommonBase.Models;
using Grpc.Core;
using Microsoft.AspNetCore.Http;
using NotificationService.Interfaces;
using ServiceCatalog.Grpc;
using Service = ServiceCatalog.Grpc.Service;

namespace NotificationService.Services
{
    public class ServiceCatalogServiceClient : IServiceCatalogService // Your interface
    {
        private readonly ServiceGRPC.ServiceGRPCClient _client; // Updated client type
        private readonly ILogger<ServiceCatalogServiceClient> _logger;

        public ServiceCatalogServiceClient(ChannelBase channel, ILogger<ServiceCatalogServiceClient> logger)
        {
            _client = new ServiceGRPC.ServiceGRPCClient(channel);
            _logger = logger;
        }

        public async Task<Service> GetServiceByIdAsync(Guid id)
        {
            var request = new GetServiceRequest { Id = id.ToString() };

            try
            {
                var response = await _client.GetServiceAsync(request);

                if (response == null)
                {
                    _logger.LogWarning($"Service not found: {id}");
                    return null; // Or throw an exception if you prefer
                }

                var service = new Service // Your domain Service class
                {
                    Id = response.Id,
                    Name = response.Name,
                    Description = response.Description,
                    Price = response.Price, // Parse string back to decimal
                    ProviderName = response.ProviderName,
                    ProviderEmail = response.ProviderEmail
                    // ... map other properties
                };

                return service;
            }
            catch (RpcException ex)
            {
                _logger.LogError(ex, $"gRPC error: {ex.StatusCode} - {ex.Message}");
                if (ex.StatusCode == StatusCode.NotFound)
                {
                    return null; // Or throw a custom exception
                }
                throw; // Re-throw other exceptions
            }
        }
    }
}
