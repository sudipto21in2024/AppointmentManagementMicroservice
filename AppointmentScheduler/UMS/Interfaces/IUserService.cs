using CommonBase.Models;

namespace UMS.Interfaces
{
    public interface IUserService
    {
        Task<Guid> RegisterUser(User user);
        // ... other user-related methods
    }
}
