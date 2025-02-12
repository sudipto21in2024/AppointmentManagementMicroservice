using CommonBase.Infrastructure.CQRS.BaseClasses;
using CommonBase.Models;

namespace UMS.CQRS.Queries
{
    public class GetUserByIdQuery : Query<User>
    {
        public Guid UserId { get; set; }
    }
}
