using CommonBase.DTO;
using CommonBase.Models;

namespace SCS.Data
{
    public interface IServiceRepository
    {
        //Task<IEnumerable<Service>> GetAllAsync();
        Task<Service?> GetByIdAsync(Guid id);
        Task<ServiceWithProvider?> GetServiceWithProviderAsync(Guid id);
        Task<Guid> CreateAsync(Service service);
        Task UpdateAsync(Service service);
        Task DeleteAsync(Guid id);
        //Task<IEnumerable<Service>> GetAllAsync(int pageIndex = 0, int pageSize = 10, string? keyword = null, Guid? categoryId = null);
        Task<ServiceResponseDTO> GetAllServicesAsync(    int pageIndex = 0,    int pageSize = 10,    string? keyword = null,    Guid? categoryId = null,    Guid? providerId = null);
        Task<List<ServiceDropdownDTO>> GetActiveServiceDropdownAsync();
    }
}
