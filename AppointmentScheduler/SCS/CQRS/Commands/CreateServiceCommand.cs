using CommonBase.Infrastructure.CQRS.BaseClasses;

namespace SCS.CQRS.Commands
{
    public class CreateServiceCommand : Command<Guid>
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int DurationInMinutes { get; set; }
        public Guid CategoryId { get; set; }
    }
}
