using CommonBase.Infrastructure.CQRS.BaseClasses;
using CommonBase.Models;

namespace SCS.CQRS.Queries
{
    public class GetCategoryByIdQuery : Query<Category>
    {
        public Guid Id { get; set; }
    }
}
