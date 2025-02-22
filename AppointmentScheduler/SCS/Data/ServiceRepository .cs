using CommonBase.Data;
using CommonBase.DTO;
using CommonBase.Models;
using Microsoft.EntityFrameworkCore;

namespace SCS.Data
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly ApplicationDbContext _context;

        public ServiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //public async Task<IEnumerable<Service>> GetAllAsync(
        //   int pageIndex = 0,
        //   int pageSize = 10,
        //   string? keyword = null,
        //   Guid? categoryId = null)
        //{
        //    IQueryable<Service> query = _context.Services.Include(s => s.Category);

        //    if (!string.IsNullOrEmpty(keyword))
        //    {
        //        query = query.Where(s => s.Name.Contains(keyword) || s.Description.Contains(keyword));
        //    }

        //    if (categoryId.HasValue)
        //    {
        //        query = query.Where(s => s.CategoryId == categoryId.Value);
        //    }

        //    return await query
        //        .Skip(pageIndex * pageSize)
        //        .Take(pageSize)
        //        .ToListAsync();
        //}
        public async Task<ServiceResponseDTO> GetAllServicesAsync(
    int pageIndex = 0,
    int pageSize = 10,
    string? keyword = null,
    Guid? categoryId = null,
    Guid? providerId = null
)
        {
            IQueryable<Service> query = _context.Services
                .Include(s => s.Provider);

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(s => s.Name.Contains(keyword) || s.Description.Contains(keyword));
            }

            if (categoryId.HasValue)
            {
                query = query.Where(s => s.CategoryId == categoryId.Value);
            }

            if (providerId.HasValue)
            {
                query = query.Where(s => s.ProviderId == providerId.Value);
            }
            query = query.Where(s => s.IsActive == true);
            var totalRecords = await query.CountAsync();

            var services = await query
                .OrderBy(s => s.Name)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .Select(s => new ServiceDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    Category = s.Category.Name, // Access Category name via navigation property
                    Price = s.Price,
                    Duration = s.DurationInMinutes, // Correct property name
                    Provider = s.Provider.BusinessName,
                    IsActive = s.IsActive
                })
                .ToListAsync();

            return new ServiceResponseDTO
            {
                Data = services,
                TotalRecords = totalRecords
            };
        }
        public async Task<List<ServiceDropdownDTO>> GetActiveServiceDropdownAsync()
        {
            return await _context.Services
                .Where(s => s.IsActive) // Filter by IsActive = true
                .Select(s => new ServiceDropdownDTO
                {
                    Id = s.Id,
                    Name = s.Name
                })
                .OrderBy(s => s.Name) // Order by name for better dropdown experience
                .ToListAsync();
        }

        public async Task<Service?> GetByIdAsync(Guid id)
        {
            return await _context.Services.FindAsync(id);
        }

        public async Task<Guid> CreateAsync(Service service)
        {
            await _context.Services.AddAsync(service);
            await _context.SaveChangesAsync();
            return service.Id;
        }

        public async Task UpdateAsync(Service service)
        {
            _context.Entry(service).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service != null)
            {
                _context.Services.Remove(service);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ServiceWithProvider> GetServiceWithProviderAsync(Guid id)
        {
            return await _context.Services
                .Include(s => s.Provider) // Assuming you have a Provider navigation property
                .ThenInclude(u => u.User)
                .Where(s => s.Id == id)
                .Select(s => new ServiceWithProvider // Project into DTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    Price = s.Price,
                    ProviderName = s.Provider != null ? s.Provider.BusinessName : "N/A", // Default to "N/A" if null
                    ProviderEmail = s.Provider != null && s.Provider.User != null ? s.Provider.User.Email : "N/A"
                })
                .FirstOrDefaultAsync();
        }
    }
}
