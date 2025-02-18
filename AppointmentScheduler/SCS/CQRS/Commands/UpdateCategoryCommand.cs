using CommonBase.Infrastructure.CQRS.BaseClasses;
using MediatR;

namespace SCS.CQRS.Commands
{
    public class UpdateCategoryCommand : Command<Unit>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
