using CommonBase.DTO;
using CommonBase.Models;

namespace SCS.Services
{
    public interface IServiceCatalogService
    {
        Task<Guid> CreateServiceAsync(Service service);
        Task DeleteServiceAsync(Guid id);
        Task<ServiceResponseDTO> GetAllServicesAsync();
        Task<Service?> GetServiceByIdAsync(Guid id);
        Task<bool> ServiceExistsAsync(Guid id);
        Task UpdateServiceAsync(Service service);
    }
}