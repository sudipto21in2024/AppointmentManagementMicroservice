using CommonBase.Infrastructure.CQRS.BaseClasses;
using MediatR;

namespace SCS.CQRS.Commands
{
    public class DeleteCategoryCommand : Command<Unit>
    {
        public Guid Id { get; set; }
    }
}
