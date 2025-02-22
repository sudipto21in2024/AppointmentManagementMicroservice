using CommonBase.DTO;
using CommonBase.Infrastructure.CQRS.BaseClasses;
using CommonBase.Models;

namespace SCS.CQRS.Queries
{
    public class GetAllServicesQuery : Query<ServiceResponseDTO>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Keyword { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? ProviderId { get; set; }
        public bool? IsActive { get; set; }
    }

    public class GetServiceDropdownQuery : Query<List<ServiceDropdownDTO>>
    {
        // No parameters needed for this query
    }

}
