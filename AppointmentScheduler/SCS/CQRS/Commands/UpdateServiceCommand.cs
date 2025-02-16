using CommonBase.Infrastructure.CQRS.BaseClasses;
using MediatR;

namespace SCS.CQRS.Commands
{
    public class UpdateServiceCommand : Command<Unit>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int DurationInMinutes { get; set; }
        public Guid CategoryId { get; set; }
    }
}
