using CommonBase.Infrastructure.CQRS.BaseClasses;
using CommonBase.Models;

namespace SCS.CQRS.Queries
{
    public class GetAllServicesQuery : Query<IEnumerable<Service>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public string? Keyword { get; set; } = null;
        public Guid? CategoryId { get; set; } = null;
    }
}
