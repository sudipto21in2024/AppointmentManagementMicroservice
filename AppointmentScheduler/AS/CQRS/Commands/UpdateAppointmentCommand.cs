using CommonBase.Infrastructure.CQRS.BaseClasses;
using MediatR;

namespace AS.CQRS.Commands
{
    public class UpdateAppointmentCommand : Command<Unit>
    {
        public Guid Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid ServiceId { get; set; }
        public Guid CustomerId { get; set; }
    }
}
