

namespace NotificationService.Interfaces
{
    public interface IServiceCatalogService
    {
        Task<ServiceCatalog.Grpc.Service> GetServiceByIdAsync(Guid id);
        
    }
}
