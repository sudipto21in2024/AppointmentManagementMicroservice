using CommonBase.Models;

namespace UMS.Interfaces
{
    public interface IRegisteredUserService
    {
        Task<Guid> RegisterUser(User user);
        // ... other user-related methods
    }
}
