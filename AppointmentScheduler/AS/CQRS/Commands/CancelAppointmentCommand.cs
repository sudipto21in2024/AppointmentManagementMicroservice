using CommonBase.Infrastructure.CQRS.BaseClasses;
using MediatR;

namespace AS.CQRS.Commands
{
    public class CancelAppointmentCommand : Command<Unit>
    {
        public Guid Id { get; set; }
    }
}
