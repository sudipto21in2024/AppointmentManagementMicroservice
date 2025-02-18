using CommonBase.Infrastructure.CQRS.BaseClasses;

namespace SCS.CQRS.Commands
{
    public class CreateCategoryCommand : Command<Guid>
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; }
    }
}
