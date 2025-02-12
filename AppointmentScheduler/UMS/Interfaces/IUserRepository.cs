using CommonBase.Models;

namespace UMS.Interfaces
{
  
        public interface IUserRepository
        {
            Task<User> GetByIdAsync(Guid id);
            Task<User> GetByEmailAsync(string email);
            Task<Guid> CreateAsync(User user);
            Task UpdateAsync(User user);
            Task DeleteAsync(Guid id);
            Task<IEnumerable<User>> GetAllAsync();
        }
    
}
