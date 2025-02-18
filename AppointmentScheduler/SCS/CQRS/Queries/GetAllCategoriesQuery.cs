using CommonBase.Infrastructure.CQRS.BaseClasses;
using CommonBase.Models;

namespace SCS.CQRS.Queries
{
    public class GetAllCategoriesQuery : Query<IEnumerable<Category>>
    {
    }
}
