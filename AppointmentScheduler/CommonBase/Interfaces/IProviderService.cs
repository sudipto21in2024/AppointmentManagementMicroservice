using CommonBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonBase.Interfaces
{
    public interface IProviderService
    {
        Task<IEnumerable<Provider>> GetAllAsync();
        Task<Provider> GetByIdAsync(Guid id);
        Task<Provider> CreateAsync(Provider provider);
        Task UpdateAsync(Provider provider);
        Task DeleteAsync(Guid id);
    }
}
