using CommonBase.Models;

namespace NotificationService.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(Guid id);
    }
}
