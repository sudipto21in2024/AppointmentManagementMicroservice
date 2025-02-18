using CommonBase.Infrastructure.CQRS.BaseClasses;
using MediatR;

namespace AS.CQRS.Commands
{
  
    public class ConfirmAppointmentCommand : Command<Unit>
    {
        public Guid Id { get; set; }
    }
}
