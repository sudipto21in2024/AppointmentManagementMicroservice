using CommonBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonBase.Interfaces
{
    public interface IServiceService
    {
        Task<IEnumerable<Service>> GetAllAsync();
        Task<Service> GetByIdAsync(Guid id);
        Task<Service> CreateAsync(Service service);
        Task UpdateAsync(Service service);
        Task DeleteAsync(Guid id);
    }
}
