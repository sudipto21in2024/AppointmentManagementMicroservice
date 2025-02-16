using CommonBase.Infrastructure.CQRS.BaseClasses;
using CommonBase.Models;

namespace SCS.CQRS.Queries
{
    public class GetServiceByIdQuery : Query<Service>
    {
        public Guid Id { get; set; }
    }
}
